using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS_Core
{
    public class World
    {
        // Main cache storage - we use component type ID as the key for faster lookup

        #region Static Access
        private static World activeWorld;
        public static World Active => activeWorld;

        public static void SetActiveWorld(World world) => activeWorld = world;
        #endregion

        #region Constants
        private const int CLEANUP_THRESHOLD = 100;
        private const int FIRST_NORMAL_ENTITY_ID = 1000;
        private const int OPERATIONS_PER_FRAME = 100;
        #endregion

        #region Fields
        private readonly ArchetypeManager archetypeManager;
        private readonly Dictionary<int, Archetype> entityArchetypes = new();
        private readonly Dictionary<int, Dictionary<ComponentType, object>> pendingComponents =
            new();
        private readonly Dictionary<string, Dictionary<ComponentType, object>> templates = new();
        private readonly Dictionary<string, Archetype> archetypeCache = new();
        private readonly Queue<int> pendingDestructions = new();
        private readonly HashSet<int> markedForDestruction = new();
        private readonly Queue<(string template, Action<int> modifier)> pendingCreations = new();
        private readonly QueryCacheManager queryCacheManager;
        private readonly SingletonCache singletonCache;
        private StructuralChangeVersion StructuralVersion;
        private uint currentFrame;

        private int nextEntityId = FIRST_NORMAL_ENTITY_ID;
        private int pendingOperationCount = 0;
        private int creationBatchSize = 0;
        #endregion

        public ArchetypeManager ArchetypeManager => archetypeManager;

        public StructuralChangeVersion GetStructuralVersion() => StructuralVersion;

        public World()
        {
            archetypeManager = new ArchetypeManager(this);
            queryCacheManager = new QueryCacheManager(this, archetypeManager);
            singletonCache = new SingletonCache(this);
        }

        public void IncrementChange()
        {
            currentFrame++;
            StructuralVersion.FrameNumber = currentFrame;
        }

        private void NotifyStructuralChange()
        {
            StructuralVersion.IncrementVersion();
            queryCacheManager.MarkAllDirty();
        }

        #region Entity Creation and Components

        public int CreateEntity()
        {
            var entityId = nextEntityId++;
            pendingComponents[entityId] = new Dictionary<ComponentType, object>();
            return entityId;
        }

        public void AddComponent<T>(int entityId, T component)
            where T : struct, IComponent
        {
            if (!pendingComponents.TryGetValue(entityId, out var components))
            {
                components = new Dictionary<ComponentType, object>();
                pendingComponents[entityId] = components;
            }

            components[ComponentType.Of<T>()] = component;
            NotifyStructuralChange();
        }

        public void UpdateEntity(int entityId)
        {
            if (!pendingComponents.TryGetValue(entityId, out var components))
                return;

            // Check singleton uniqueness
            bool isSingleton = components.ContainsKey(ComponentType.Of<SingletonFlag>());
            if (isSingleton)
            {
                foreach (var componentType in components.Keys)
                {
                    if (componentType.Type == typeof(SingletonFlag))
                        continue;

                    bool singletonExists = false;
                    var query = new QueryDescription<SingletonFlag>();
                    query.ForEach(
                        this,
                        (int existingId, ref SingletonFlag _) =>
                        {
                            if (!IsMarkedForDestruction(existingId))
                            {
                                var existingArchetype = entityArchetypes[existingId];
                                if (existingArchetype.HasComponent(componentType))
                                    singletonExists = true;
                            }
                        }
                    );

                    if (singletonExists)
                        throw new InvalidOperationException(
                            $"Singleton of type {componentType.Type.Name} already exists"
                        );
                }
            }

            var types = components.Keys.ToArray();
            var archetype = archetypeManager.GetOrCreateArchetype(types);
            archetype.Add(entityId, components);
            entityArchetypes[entityId] = archetype;
            pendingComponents.Remove(entityId);

            // Add this line to invalidate caches
            queryCacheManager.MarkAllDirty();
            singletonCache.MarkDirty();
        }

        public void UpdateEntities(IEnumerable<int> entityIds)
        {
            var entitiesByArchetype =
                new Dictionary<
                    string,
                    List<(int id, Dictionary<ComponentType, object> components)>
                >();
            // Debug count of entities to update
            int totalEntities = entityIds.Count();
            Debug.Log($"UpdateEntities called with {totalEntities} entities");
            foreach (var entityId in entityIds)
            {
                if (!pendingComponents.TryGetValue(entityId, out var components))
                    continue;

                var typeKey = string.Join(",", components.Keys.Select(t => t.Id).OrderBy(id => id));
                if (!entitiesByArchetype.TryGetValue(typeKey, out var list))
                {
                    list = new List<(int, Dictionary<ComponentType, object>)>();
                    entitiesByArchetype[typeKey] = list;
                }
                list.Add((entityId, components));
            }

            Debug.Log($"Grouped into {entitiesByArchetype.Count} archetypes");

            foreach (var group in entitiesByArchetype)
            {
                Archetype archetype;
                if (!archetypeCache.TryGetValue(group.Key, out archetype))
                {
                    var types = group.Value[0].components.Keys.ToArray();
                    Debug.Log(
                        $"Creating new archetype with types: {string.Join(", ", types.Select(t => t.Type.Name))}"
                    ); // Add this
                    archetype = archetypeManager.GetOrCreateArchetype(types);
                    archetypeCache[group.Key] = archetype;
                }

                foreach (var (entityId, components) in group.Value)
                {
                    Debug.Log($"Adding entity {entityId} to archetype"); // Add this
                    archetype.Add(entityId, components);
                    entityArchetypes[entityId] = archetype;
                    pendingComponents.Remove(entityId);
                }
            }
            queryCacheManager.MarkAllDirty();
        }
        #endregion

        #region Template System
        public void CreateTemplate(
            string templateName,
            Dictionary<ComponentType, object> components
        )
        {
            templates[templateName] = components;

            var types = components.Keys.ToArray();
            var typeKey = string.Join(",", types.Select(t => t.Id).OrderBy(id => id));
            if (!archetypeCache.ContainsKey(typeKey))
            {
                archetypeCache[typeKey] = archetypeManager.GetOrCreateArchetype(types);
            }
        }

        public int CreateEntityFromTemplate(string templateName)
        {
            if (!templates.TryGetValue(templateName, out var templateComponents))
                throw new ArgumentException($"Template {templateName} not found");

            var entityId = CreateEntity();
            var components = new Dictionary<ComponentType, object>(templateComponents);
            pendingComponents[entityId] = components;
            queryCacheManager.MarkAllDirty();
            return entityId;
        }

        public void QueueEntityCreation(string templateName, Action<int> modifyComponents = null)
        {
            pendingCreations.Enqueue((templateName, modifyComponents));
            creationBatchSize++;
        }

        public void ModifyPendingComponent<T>(int entityId, ActionRef<T> modifier)
            where T : struct, IComponent
        {
            if (!pendingComponents.TryGetValue(entityId, out var components))
                return;

            var componentType = ComponentType.Of<T>();
            if (components.TryGetValue(componentType, out var component))
            {
                var typed = (T)component;
                modifier(ref typed);
                components[componentType] = typed;
            }
        }
        #endregion

        #region Entity Management
        public void DestroyEntityDeferred(int entityId)
        {
            if (!markedForDestruction.Contains(entityId))
            {
                pendingDestructions.Enqueue(entityId);
                markedForDestruction.Add(entityId);
                NotifyStructuralChange();
            }
            singletonCache.MarkDirty();
        }

        public bool EntityExists(int entityId)
        {
            return entityArchetypes.ContainsKey(entityId)
                && !markedForDestruction.Contains(entityId);
        }

        public bool IsMarkedForDestruction(int entityId) => markedForDestruction.Contains(entityId);
        #endregion

        #region Query System
        // Add this method to access the cache
        internal QueryCache GetQueryCache(ComponentType[] types)
        {
            return queryCacheManager.GetOrCreateCache(types);
        }

        public static QueryDescription<T1> Query<T1>()
            where T1 : struct, IComponent
        {
            return new QueryDescription<T1>();
        }

        public static QueryDescription<T1, T2> Query<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            return new QueryDescription<T1, T2>();
        }

        public static QueryDescription<T1, T2, T3> Query<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            return new QueryDescription<T1, T2, T3>();
        }

        public static QueryDescription<T1, T2, T3, T4> Query<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            return new QueryDescription<T1, T2, T3, T4>();
        }
        #endregion

        #region Singleton Access

        // Public API - TFlag is the singleton identifier, T1 is the component we want
        public void GetSingletonComponents<TFlag, T1>(out TFlag flag, out T1 component1)
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
        {
            singletonCache.GetComponents<TFlag, T1>(out flag, out component1);
        }

        // Overload for two components
        public void GetSingletonComponents<TFlag, T1, T2>(
            out TFlag flag,
            out T1 component1,
            out T2 component2
        )
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            singletonCache.GetComponents<TFlag, T1, T2>(out flag, out component1, out component2);
        }

        // Overload for three components
        public void GetSingletonComponents<TFlag, T1, T2, T3>(
            out T1 component1,
            out T2 component2,
            out T3 component3
        )
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            singletonCache.GetComponents<TFlag, T1, T2, T3>(
                out component1,
                out component2,
                out component3
            );
        }

        #endregion

        #region Update System
        public void Update()
        {
            ProcessPendingCreations();
            ProcessPendingDestructions();
        }

        private void ProcessPendingCreations()
        {
            if (creationBatchSize == 0)
                return;

            var entitiesToUpdate = new List<int>();
            var processCount = Math.Min(OPERATIONS_PER_FRAME, creationBatchSize);

            for (int i = 0; i < processCount; i++)
            {
                if (pendingCreations.Count == 0)
                    break;

                var (template, modifier) = pendingCreations.Dequeue();
                int entityId = CreateEntityFromTemplate(template);

                modifier?.Invoke(entityId);
                entitiesToUpdate.Add(entityId);
            }

            if (entitiesToUpdate.Count > 0)
            {
                UpdateEntities(entitiesToUpdate);
                creationBatchSize -= entitiesToUpdate.Count;
            }
        }

        private void ProcessPendingDestructions()
        {
            int processCount = Math.Min(OPERATIONS_PER_FRAME, pendingDestructions.Count);
            bool anyProcessed = false;

            for (int i = 0; i < processCount; i++)
            {
                if (pendingDestructions.Count == 0)
                    break;

                int entityId = pendingDestructions.Dequeue();
                markedForDestruction.Remove(entityId);

                if (entityArchetypes.TryGetValue(entityId, out var archetype))
                {
                    archetype.RemoveEntity(entityId);
                    entityArchetypes.Remove(entityId);
                    pendingOperationCount++;
                    anyProcessed = true;
                }
            }

            if (anyProcessed)
            {
                // Add this line when entities are actually destroyed
                queryCacheManager.MarkAllDirty();
            }

            if (pendingOperationCount >= CLEANUP_THRESHOLD)
            {
                archetypeManager.CleanupEmptyArchetypes();
                pendingOperationCount = 0;
                // Add this line after cleanup
                queryCacheManager.MarkAllDirty();
            }
        }

        // Add cleanup for query cache
        public void Clear()
        {
            // Existing cleanup logic...
            queryCacheManager.MarkAllDirty();
            singletonCache.Clear();
        }

        public (int creations, int destructions) GetPendingOperationCounts() =>
            (creationBatchSize, pendingDestructions.Count);
        #endregion
    }

    public struct SingletonFlag : IComponent { }
}