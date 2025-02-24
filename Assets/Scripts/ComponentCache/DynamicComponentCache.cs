using System.Collections.Generic;
using UnityEngine;

namespace ComponentCache.Core
{
    public class DynamicComponentCache<T>
        where T : Component
    {
        private T[] cache;
        private int currentCapacity;
        private const int MINIMUM_CAPACITY = 32;

        // Our "recycling bin" for freed indices
        private Queue<int> freedIndices;

        // Track the highest used index for optimization
        private int highestUsedIndex;

        public DynamicComponentCache(int initialCapacity = MINIMUM_CAPACITY)
        {
            initialCapacity = Mathf.Max(initialCapacity, MINIMUM_CAPACITY);
            cache = new T[initialCapacity];
            currentCapacity = initialCapacity;
            freedIndices = new Queue<int>();
            highestUsedIndex = -1;
        }

        public int Set(T component)
        {
            int index;
            if (freedIndices.Count > 0)
            {
                // Reuse a freed index if available
                index = freedIndices.Dequeue();
                cache[index] = component;
                return index;
            }

            // No holes available, use next available index
            index = highestUsedIndex + 1;
            ResizeIfNeeded(index + 1);
            cache[index] = component;
            highestUsedIndex = index;
            return index;
        }

        public void Unregister(int index)
        {
            if (index < 0 || index >= currentCapacity)
                return;

            // Clear the component and add index to our recycling queue
            cache[index] = null;
            freedIndices.Enqueue(index);

            // Update highestUsedIndex if we're removing the highest element
            if (index == highestUsedIndex)
            {
                // Find the new highest used index
                while (highestUsedIndex >= 0 && cache[highestUsedIndex] == null)
                {
                    highestUsedIndex--;
                }
            }
        }

        public T Get(int index)
        {
            // Safety check - return null if index is out of bounds
            if (index >= currentCapacity)
                return null;
            return cache[index];
        }

        public void Clear(int index)
        {
            if (index < currentCapacity)
            {
                cache[index] = null;
            }
        }

        private void ResizeIfNeeded(int requiredSize)
        {
            if (requiredSize >= currentCapacity)
            {
                // Calculate new size: double until we meet requirement
                int newSize = currentCapacity;
                while (newSize <= requiredSize)
                {
                    newSize *= 2;
                }

                // Create and copy to new array
                T[] newCache = new T[newSize];
                System.Array.Copy(cache, newCache, currentCapacity);
                cache = newCache;
                currentCapacity = newSize;

                Debug.Log($"Cache for {typeof(T).Name} resized to {newSize} slots");
            }
        }

        public void TrimExcess()
        {
            // Optional: Shrink array if we're using much less than capacity
            // Could be called during scene transitions or quiet periods
            int usedSlots = 0;
            for (int i = 0; i < currentCapacity; i++)
            {
                if (cache[i] != null)
                    usedSlots = i + 1;
            }

            if (usedSlots < currentCapacity / 2)
            {
                int newSize = Mathf.Max(usedSlots * 2, MINIMUM_CAPACITY);
                T[] newCache = new T[newSize];
                System.Array.Copy(cache, newCache, usedSlots);
                cache = newCache;
                currentCapacity = newSize;
            }
        }
    }
}
