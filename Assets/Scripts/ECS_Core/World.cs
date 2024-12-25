using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ECS_Core
{
    public class World
    {
        private const int CLEANUP_THRESHOLD = 100; // Adjust based on your needs
        private int operationCount = 0;
        private readonly ArchetypeManager archetypeManager = new();
        private int nextEntityId = 0;
        private Dictionary<int, Archetype> entityArchetypes = new();

        public ArchetypeManager ArchetypeManager => archetypeManager;

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

        public int CreateEntity<T1>(T1 component1)
            where T1 : struct, IComponent
        {
            var entityId = nextEntityId++;

            var types = new[] { ComponentType.Of<T1>() };
            var components = new Dictionary<ComponentType, object>
            {
                { ComponentType.Of<T1>(), component1 },
            };

            var archetype = archetypeManager.GetOrCreateArchetype(types);
            archetype.Add(entityId, components);
            entityArchetypes[entityId] = archetype;

            return entityId;
        }

        public int CreateEntity<T1, T2>(T1 component1, T2 component2)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var entityId = nextEntityId++;

            var types = new[] { ComponentType.Of<T1>(), ComponentType.Of<T2>() };

            var components = new Dictionary<ComponentType, object>
            {
                { ComponentType.Of<T1>(), component1 },
                { ComponentType.Of<T2>(), component2 },
            };

            var archetype = archetypeManager.GetOrCreateArchetype(types);
            archetype.Add(entityId, components);
            entityArchetypes[entityId] = archetype;

            return entityId;
        }

        public int CreateEntity<T1, T2, T3>(T1 component1, T2 component2, T3 component3)
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var entityId = nextEntityId++;

            var types = new[]
            {
                ComponentType.Of<T1>(),
                ComponentType.Of<T2>(),
                ComponentType.Of<T3>(),
            };

            var components = new Dictionary<ComponentType, object>
            {
                { ComponentType.Of<T1>(), component1 },
                { ComponentType.Of<T2>(), component2 },
                { ComponentType.Of<T3>(), component3 },
            };

            var archetype = archetypeManager.GetOrCreateArchetype(types);
            archetype.Add(entityId, components);
            entityArchetypes[entityId] = archetype;

            return entityId;
        }

        public int CreateEntity<T1, T2, T3, T4>(
            T1 component1,
            T2 component2,
            T3 component3,
            T4 component4
        )
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
            where T4 : struct, IComponent
        {
            var entityId = nextEntityId++;

            var types = new[]
            {
                ComponentType.Of<T1>(),
                ComponentType.Of<T2>(),
                ComponentType.Of<T3>(),
                ComponentType.Of<T4>(),
            };

            var components = new Dictionary<ComponentType, object>
            {
                { ComponentType.Of<T1>(), component1 },
                { ComponentType.Of<T2>(), component2 },
                { ComponentType.Of<T3>(), component3 },
                { ComponentType.Of<T4>(), component4 },
            };

            var archetype = archetypeManager.GetOrCreateArchetype(types);
            archetype.Add(entityId, components);
            entityArchetypes[entityId] = archetype;

            return entityId;
        }

        public void DestroyEntity(int entityId)
        {
            if (!entityArchetypes.TryGetValue(entityId, out var archetype))
                return;

            archetype.RemoveEntity(entityId);
            entityArchetypes.Remove(entityId);

            // Periodic cleanup
            operationCount++;
            if (operationCount >= CLEANUP_THRESHOLD)
            {
                archetypeManager.CleanupEmptyArchetypes();
                operationCount = 0;
            }
        }

        public bool EntityExists(int entityId)
        {
            return entityArchetypes.ContainsKey(entityId);
        }

        public void AddComponent<T>(int entityId, T component)
            where T : struct, IComponent
        {
            if (!entityArchetypes.TryGetValue(entityId, out var currentArchetype))
                return;

            var newType = ComponentType.Of<T>();
            if (currentArchetype.HasComponent(newType))
                return; // Component already exists

            // Create new archetype
            var newTypes = new ComponentType[currentArchetype.ComponentTypes.Count() + 1];
            var index = 0;
            foreach (var type in currentArchetype.ComponentTypes)
            {
                newTypes[index++] = type;
            }
            newTypes[index] = newType;

            var newArchetype = archetypeManager.GetOrCreateArchetype(newTypes);

            // Transfer existing components directly
            var sourceIndex = currentArchetype.GetEntityIndex(entityId);
            foreach (var type in currentArchetype.ComponentTypes)
            {
                var sourceArray = currentArchetype.GetComponentArrayRaw(type);
                var targetArray = newArchetype.GetComponentArrayRaw(type);
                var elementSize = Marshal.SizeOf(type.Type);

                Buffer.BlockCopy(
                    sourceArray,
                    sourceIndex * elementSize,
                    targetArray,
                    newArchetype.Count * elementSize,
                    elementSize
                );
            }

            // Add new component directly to its array
            var newComponentArray = newArchetype.GetComponentArray<T>();
            newComponentArray[newArchetype.Count] = component;

            // Update tracking
            currentArchetype.RemoveEntity(entityId);
            newArchetype.AddWithoutComponents(entityId); // No more empty dictionary allocation
            entityArchetypes[entityId] = newArchetype;
        }

        public void RemoveComponent<T>(int entityId)
            where T : struct, IComponent
        {
            if (!entityArchetypes.TryGetValue(entityId, out var currentArchetype))
                return;

            var typeToRemove = ComponentType.Of<T>();
            if (!currentArchetype.HasComponent(typeToRemove))
                return;

            // Create new archetype type array (excluding removed type)
            var newTypes = new ComponentType[currentArchetype.ComponentTypes.Count() - 1];
            var index = 0;
            foreach (var type in currentArchetype.ComponentTypes)
            {
                if (type.Type != typeof(T))
                    newTypes[index++] = type;
            }

            var newArchetype = archetypeManager.GetOrCreateArchetype(newTypes);

            // Transfer components directly
            var sourceIndex = currentArchetype.GetEntityIndex(entityId);
            foreach (var type in newTypes)
            {
                var sourceArray = currentArchetype.GetComponentArrayRaw(type);
                var targetArray = newArchetype.GetComponentArrayRaw(type);
                var elementSize = Marshal.SizeOf(type.Type);

                Buffer.BlockCopy(
                    sourceArray,
                    sourceIndex * elementSize,
                    targetArray,
                    newArchetype.Count * elementSize,
                    elementSize
                );
            }

            // Update tracking
            currentArchetype.RemoveEntity(entityId);
            newArchetype.AddWithoutComponents(entityId); // No more empty dictionary allocation
            entityArchetypes[entityId] = newArchetype;
        }

        public void AddComponentToEntities<T>(IEnumerable<int> entityIds, T component)
            where T : struct, IComponent
        {
            // Group entities by current archetype to minimize archetype creation
            var groupedEntities = entityIds.Where(EntityExists).GroupBy(id => entityArchetypes[id]);

            foreach (var group in groupedEntities)
            {
                var sourceArchetype = group.Key;
                var newType = ComponentType.Of<T>();

                if (sourceArchetype.HasComponent(newType))
                    continue;

                // Create new archetype once for this group
                var newTypes = new ComponentType[sourceArchetype.ComponentTypes.Count() + 1];
                var index = 0;
                foreach (var type in sourceArchetype.ComponentTypes)
                {
                    newTypes[index++] = type;
                }
                newTypes[index] = newType;

                var targetArchetype = archetypeManager.GetOrCreateArchetype(newTypes);

                // Transfer all entities in this group
                foreach (var entityId in group)
                {
                    var sourceIndex = sourceArchetype.GetEntityIndex(entityId);

                    // Transfer existing components
                    foreach (var type in sourceArchetype.ComponentTypes)
                    {
                        var sourceArray = sourceArchetype.GetComponentArrayRaw(type);
                        var targetArray = targetArchetype.GetComponentArrayRaw(type);
                        var elementSize = Marshal.SizeOf(type.Type);

                        Buffer.BlockCopy(
                            sourceArray,
                            sourceIndex * elementSize,
                            targetArray,
                            targetArchetype.Count * elementSize,
                            elementSize
                        );
                    }

                    // Add new component
                    var newComponentArray = targetArchetype.GetComponentArray<T>();
                    newComponentArray[targetArchetype.Count] = component;

                    // Update tracking with the new method instead
                    sourceArchetype.RemoveEntity(entityId);
                    targetArchetype.AddWithoutComponents(entityId); // No more empty dictionary allocation
                    entityArchetypes[entityId] = targetArchetype;
                }
            }
        }
    }
}
