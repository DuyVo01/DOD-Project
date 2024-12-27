using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ECS_Core
{
    public class Archetype
    {
        private const int DEFAULT_INITIAL_CAPACITY = 16;
        private const float GROWTH_FACTOR = 2.0f;
        private readonly HashSet<ComponentType> componentTypes = new();
        private readonly Dictionary<ComponentType, Array> componentArrays = new();
        private readonly List<int> entities = new();
        private readonly Dictionary<int, int> entityToIndex = new();
        private int count = 0;

        public Archetype(ComponentType[] types)
        {
            foreach (var type in types)
            {
                componentTypes.Add(type);
                componentArrays[type] = Array.CreateInstance(type.Type, DEFAULT_INITIAL_CAPACITY); // Initial capacity
            }
        }

        public bool HasComponent(ComponentType type) => componentTypes.Contains(type);

        public T[] GetComponentArray<T>()
            where T : struct, IComponent
        {
            var type = ComponentType.Of<T>();
            return (T[])componentArrays[type];
        }

        public void Add(int entityId, Dictionary<ComponentType, object> components)
        {
            if (count == componentArrays.First().Value.Length)
            {
                Grow();
            }

            entityToIndex[entityId] = count;
            entities.Add(entityId);

            foreach (var kvp in components)
            {
                var array = componentArrays[kvp.Key];
                Array.Copy(new[] { kvp.Value }, 0, array, count, 1);
            }

            count++;
        }

        // New overload for when components are already copied
        public void AddWithoutComponents(int entityId)
        {
            if (count == componentArrays.First().Value.Length)
            {
                Grow();
            }

            entityToIndex[entityId] = count;
            entities.Add(entityId);
            count++;
        }

        public void RemoveEntity(int entityId)
        {
            if (!entityToIndex.TryGetValue(entityId, out int indexToRemove))
                return;

            int lastIndex = count - 1;
            if (indexToRemove < lastIndex)
            {
                int lastEntityId = entities[lastIndex];

                foreach (var componentArray in componentArrays.Values)
                {
                    Array.Copy(componentArray, lastIndex, componentArray, indexToRemove, 1);
                }

                entityToIndex[lastEntityId] = indexToRemove;
                entities[indexToRemove] = lastEntityId;
            }

            entityToIndex.Remove(entityId);
            entities.RemoveAt(count - 1);
            count--;
        }

        private void Grow()
        {
            var newCapacity = (int)(componentArrays.First().Value.Length * GROWTH_FACTOR);
            foreach (var type in componentArrays.Keys.ToList())
            {
                var oldArray = componentArrays[type];
                var newArray = Array.CreateInstance(type.Type, newCapacity);

                // Use Array.Copy instead of Buffer.BlockCopy
                // This properly handles copying of value types (structs)
                Array.Copy(oldArray, 0, newArray, 0, count);

                componentArrays[type] = newArray;
            }
        }

        public bool HasEntity(int entityId) => entityToIndex.ContainsKey(entityId);

        public int Count => count;
        public IReadOnlyList<int> Entities => entities;

        public IEnumerable<ComponentType> ComponentTypes => componentTypes;

        public Array GetComponentArrayRaw(ComponentType type)
        {
            if (!componentArrays.TryGetValue(type, out var array))
                throw new ArgumentException(
                    $"ComponentType {type.Type.Name} not found in archetype."
                );

            return array;
        }

        public int GetEntityIndex(int entityId)
        {
            if (!entityToIndex.TryGetValue(entityId, out var index))
                throw new ArgumentException($"Entity {entityId} not found in archetype.");

            return index;
        }

        public void TransferEntityComponents(
            int entityId,
            Archetype targetArchetype,
            HashSet<ComponentType> componentTypesToTransfer
        )
        {
            // Get source entity index
            var sourceIndex = GetEntityIndex(entityId);
            if (sourceIndex == -1)
                throw new ArgumentException($"Entity {entityId} not found in source archetype.");

            foreach (var type in componentTypesToTransfer)
            {
                if (
                    !componentArrays.ContainsKey(type)
                    || !targetArchetype.componentArrays.ContainsKey(type)
                )
                    continue;

                var sourceArray = componentArrays[type];
                var targetArray = targetArchetype.componentArrays[type];
                var elementSize = Marshal.SizeOf(type.Type);

                // Ensure target has space
                if (targetArchetype.count >= targetArray.Length)
                    targetArchetype.Grow();

                // Direct memory copy for the component
                Buffer.BlockCopy(
                    sourceArray, // Source array
                    sourceIndex * elementSize, // Source offset
                    targetArray, // Target array
                    targetArchetype.count * elementSize, // Target offset
                    elementSize // How many bytes to copy
                );
            }
        }
    }
}
