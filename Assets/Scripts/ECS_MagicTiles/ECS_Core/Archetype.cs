using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ECS_Core
{
    public class Archetype
    {
        private const int DEFAULT_INITIAL_CAPACITY = 16;
        private readonly HashSet<ComponentType> componentTypes = new();
        private readonly Dictionary<ComponentType, SparseSet<object>> componentSets = new();
        private readonly List<int> entities = new();
        private readonly Dictionary<int, int> entityToIndex = new();
        private int count = 0;

        public Archetype(ComponentType[] types)
        {
            foreach (var type in types)
            {
                componentTypes.Add(type);
                componentSets[type] = new SparseSet<object>(DEFAULT_INITIAL_CAPACITY);
            }
        }

        public bool HasComponent(ComponentType type) => componentTypes.Contains(type);

        public void Add(int entityId, Dictionary<ComponentType, object> components)
        {
            // For each component type and value pair
            foreach (var kvp in components)
            {
                if (componentSets.TryGetValue(kvp.Key, out var sparseSet))
                {
                    // Add directly to sparse set
                    sparseSet.Add(entityId, kvp.Value);
                }
            }

            // Keep your existing entity tracking
            entities.Add(entityId);
            entityToIndex[entityId] = count;
            count++;
        }

        public T[] GetComponentArray<T>()
            where T : struct, IComponent
        {
            var type = ComponentType.Of<T>();
            if (!componentSets.TryGetValue(type, out var sparseSet))
            {
                throw new ArgumentException($"Component type {typeof(T)} not found in archetype");
            }

            var span = sparseSet.GetDataSpan();
            var result = new T[span.Length];
            for (int i = 0; i < span.Length; i++)
            {
                result[i] = (T)span[i];
            }
            return result;
        }

        public void RemoveEntity(int entityId)
        {
            foreach (var sparseSet in componentSets.Values)
            {
                sparseSet.Remove(entityId);
            }

            entities.Remove(entityId);
            entityToIndex.Remove(entityId);
            count--;
        }

        public bool HasEntity(int entityId)
        {
            var firstSet = componentSets.Values.GetEnumerator().Current;
            return firstSet?.Contains(entityId) ?? false;
        }

        public int Count => count;
        public IReadOnlyList<int> Entities => entities;

        public IEnumerable<ComponentType> ComponentTypes => componentTypes;

        public Array GetComponentArrayRaw(ComponentType type)
        {
            if (!componentSets.TryGetValue(type, out var sparseSet))
                throw new ArgumentException(
                    $"ComponentType {type.Type.Name} not found in archetype."
                );

            // Convert to array for compatibility
            var span = sparseSet.GetDataSpan();
            var array = Array.CreateInstance(type.Type, span.Length);
            for (int i = 0; i < span.Length; i++)
            {
                array.SetValue(span[i], i);
            }
            return array;
        }

        public int GetEntityIndex(int entityId)
        {
            if (!entityToIndex.TryGetValue(entityId, out var index))
                throw new ArgumentException($"Entity {entityId} not found in archetype.");

            return index;
        }

        // Get all entities in this archetype
        public ReadOnlySpan<int> GetEntities()
        {
            // Since we're using SparseSet, we can get the dense array of entities
            // from any component set (they all track the same entities)
            SparseSet<object> firstSet = componentSets.Values.FirstOrDefault();
            if (firstSet != null)
            {
                return firstSet.GetEntityIds();
            }

            // Fallback to empty span if no components (shouldn't happen in practice)
            return ReadOnlySpan<int>.Empty;
        }

        // For compatibility with existing code that expects IReadOnlyList
    }
}
