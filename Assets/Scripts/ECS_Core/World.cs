using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_Core
{
    public class World
    {
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
        private readonly ArchetypeManager archetypeManager = new();
        private readonly Dictionary<int, Archetype> entityArchetypes = new();
        private readonly Dictionary<int, Dictionary<ComponentType, object>> pendingComponents =
            new();
        private readonly Dictionary<string, Dictionary<ComponentType, object>> templates = new();
        private readonly Dictionary<string, Archetype> archetypeCache = new();
        private readonly Queue<int> pendingDestructions = new();
        private readonly HashSet<int> markedForDestruction = new();
        private readonly Queue<(string template, Action<int> modifier)> pendingCreations = new();

        private int nextEntityId = FIRST_NORMAL_ENTITY_ID;
        private int pendingOperationCount = 0;
        private int creationBatchSize = 0;
        #endregion

        public ArchetypeManager ArchetypeManager => archetypeManager;

        #region Entity Creation and Components
        public struct SingletonFlag : IComponent { }

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
                        archetypeManager,
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
        }

        public void UpdateEntities(IEnumerable<int> entityIds)
        {
            var entitiesByArchetype =
                new Dictionary<
                    string,
                    List<(int id, Dictionary<ComponentType, object> components)>
                >();

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

            foreach (var group in entitiesByArchetype)
            {
                Archetype archetype;
                if (!archetypeCache.TryGetValue(group.Key, out archetype))
                {
                    var types = group.Value[0].components.Keys.ToArray();
                    archetype = archetypeManager.GetOrCreateArchetype(types);
                    archetypeCache[group.Key] = archetype;
                }

                foreach (var (entityId, components) in group.Value)
                {
                    archetype.Add(entityId, components);
                    entityArchetypes[entityId] = archetype;
                    pendingComponents.Remove(entityId);
                }
            }
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
            }
        }

        public bool EntityExists(int entityId)
        {
            return entityArchetypes.ContainsKey(entityId)
                && !markedForDestruction.Contains(entityId);
        }

        public bool IsMarkedForDestruction(int entityId) => markedForDestruction.Contains(entityId);
        #endregion

        #region Query System
        public QueryDescription<T1> Query<T1>()
            where T1 : struct, IComponent
        {
            return new QueryDescription<T1>();
        }

        public QueryDescription<T1, T2> Query<T1, T2>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            return new QueryDescription<T1, T2>();
        }

        public QueryDescription<T1, T2, T3> Query<T1, T2, T3>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            return new QueryDescription<T1, T2, T3>();
        }

        public QueryDescription<T1, T2, T3, T4> Query<T1, T2, T3, T4>()
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            return new QueryDescription<T1, T2, T3, T4>();
        }
        #endregion

        #region Singleton Access
        public ref T GetSingleton<T>()
            where T : struct, IComponent
        {
            T[] componentArray = null;
            int entityIndex = -1;

            var query = new QueryDescription<SingletonFlag, T>();
            query.ForEach(
                archetypeManager,
                (int entityId, ref SingletonFlag _, ref T component) =>
                {
                    if (!IsMarkedForDestruction(entityId))
                    {
                        var archetype = entityArchetypes[entityId];
                        componentArray = archetype.GetComponentArray<T>();
                        entityIndex = archetype.GetEntityIndex(entityId);
                    }
                }
            );

            if (componentArray == null || entityIndex == -1)
                throw new InvalidOperationException(
                    $"No singleton found with component {typeof(T).Name}"
                );

            return ref componentArray[entityIndex];
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
                }
            }

            if (pendingOperationCount >= CLEANUP_THRESHOLD)
            {
                archetypeManager.CleanupEmptyArchetypes();
                pendingOperationCount = 0;
            }
        }

        public (int creations, int destructions) GetPendingOperationCounts() =>
            (creationBatchSize, pendingDestructions.Count);
        #endregion
    }
}
