using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_MagicTile
{
    public class ArchetypeStorage
    {
        // Store component arrays indexed by their ComponentType
        private readonly Dictionary<ComponentType, Array> componentArrays;
        private int[] entityIds; // Tracks which entities are in this storage
        private readonly Dictionary<int, int> entityToIndex = new Dictionary<int, int>();
        private int count; // Number of active entities

        private const int DEFAULT_CAPACITY = 64; // Starting size for our arrays
        private const float GROWTH_FACTOR = 2.0f; // How much to grow when we need more space

        public ArchetypeStorage(Archetype archetype, int initialCapacity = DEFAULT_CAPACITY)
        {
            componentArrays = new Dictionary<ComponentType, Array>();

            // Create typed arrays for each component in the archetype
            foreach (var componentType in archetype.GetTypes())
            {
                // Creates an array of the proper type with our initial capacity
                componentArrays[componentType] = Array.CreateInstance(
                    componentType.Type,
                    initialCapacity
                );
            }

            entityIds = new int[initialCapacity];

            count = 0;
        }

        // Provides type-safe access to component arrays
        public T[] GetComponents<T>()
            where T : struct
        {
            var componentType = ComponentType.Registry.GetComponentType<T>();
            if (!componentArrays.TryGetValue(componentType, out var array))
            {
                throw new InvalidOperationException(
                    $"Component type {typeof(T)} not found in archetype"
                );
            }
            return (T[])array;
        }

        // Adds an entity with its components to this storage
        public void Add(int entityId, ReadOnlySpan<object> components)
        {
            // Grow arrays if needed
            if (count >= entityIds.Length)
            {
                Grow();
            }

            // Add components to their respective arrays
            int componentIndex = 0;
            foreach (var array in componentArrays.Values)
            {
                Array.Copy(new[] { components[componentIndex] }, 0, array, count, 1);
                componentIndex++;
            }

            // Track the entity
            entityIds[count] = entityId;
            entityToIndex[entityId] = count;
            count++;
        }

        // Efficiently removes an entity by swapping with the last element
        public void Remove(int entityId)
        {
            // Find the entity's index
            int index = Array.IndexOf(entityIds, entityId, 0, count);
            if (index == -1)
                return;

            // If it's not the last element, swap with the last one
            int lastIndex = count - 1;
            if (index < lastIndex)
            {
                foreach (var array in componentArrays.Values)
                {
                    Array.Copy(array, lastIndex, array, index, 1);
                }
                entityIds[index] = entityIds[lastIndex];
                entityToIndex[entityIds[lastIndex]] = index;
            }

            entityToIndex.Remove(entityId);
            count--;
        }

        private void Grow()
        {
            int newCapacity = (int)(entityIds.Length * GROWTH_FACTOR);

            // Grow all component arrays
            foreach (var kvp in componentArrays.ToList())
            {
                var oldArray = kvp.Value;
                var newArray = Array.CreateInstance(kvp.Key.Type, newCapacity);
                Array.Copy(oldArray, 0, newArray, 0, count);
                componentArrays[kvp.Key] = newArray;
            }

            // Grow entity ID array
            Array.Resize(ref entityIds, newCapacity);
        }

        // And we need to add this method to ArchetypeStorage
        public Array GetComponentArrayRaw(ComponentType componentType)
        {
            if (!componentArrays.TryGetValue(componentType, out var array))
            {
                throw new InvalidOperationException(
                    $"Component type {componentType.Type.Name} not found in archetype"
                );
            }
            return array;
        }

        public int GetEntityIndex(int entityId)
        {
            return entityToIndex.TryGetValue(entityId, out int index) ? index : -1;
        }

        // Properties for accessing storage information
        public int Count => count;
        public ReadOnlySpan<int> EntityIds => new ReadOnlySpan<int>(entityIds, 0, count);
    }
}
