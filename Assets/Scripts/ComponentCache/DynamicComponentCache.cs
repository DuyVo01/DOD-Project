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

        public DynamicComponentCache(int initialCapacity = MINIMUM_CAPACITY)
        {
            initialCapacity = Mathf.Max(initialCapacity, MINIMUM_CAPACITY);
            cache = new T[initialCapacity];
            currentCapacity = initialCapacity;
        }

        /// <summary>
        /// Set a component at a specific index (using GameObject's ID)
        /// </summary>
        public void Set(int index, T component)
        {
            if (index < 0)
                return;

            ResizeIfNeeded(index + 1);
            cache[index] = component;
        }

        /// <summary>
        /// Get a component at a specific index
        /// </summary>
        public T Get(int index)
        {
            if (index < 0 || index >= currentCapacity)
                return null;

            return cache[index];
        }

        /// <summary>
        /// Clear a component at a specific index
        /// </summary>
        public void Clear(int index)
        {
            if (index >= 0 && index < currentCapacity)
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

        /// <summary>
        /// Trim the array to save memory (optional)
        /// </summary>
        public void TrimExcess()
        {
            // Find the highest used index
            int highestUsedIndex = -1;
            for (int i = currentCapacity - 1; i >= 0; i--)
            {
                if (cache[i] != null)
                {
                    highestUsedIndex = i;
                    break;
                }
            }

            // If we're using less than half the capacity, resize
            if (highestUsedIndex < currentCapacity / 2)
            {
                int newSize = Mathf.Max((highestUsedIndex + 1) * 2, MINIMUM_CAPACITY);
                T[] newCache = new T[newSize];
                System.Array.Copy(cache, newCache, highestUsedIndex + 1);
                cache = newCache;
                currentCapacity = newSize;
            }
        }
    }
}
