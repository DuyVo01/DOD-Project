using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ComponentObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
        where T : Component
    {
        private readonly T[] items; // All pooled items
        private readonly bool[] activeFlags; // Active status for each item
        private readonly int[] poolIndices; // Pool indices for each item
        private readonly int[] availableIndices; // Indices of available items

        private int availableCount;
        private readonly Transform parent;
        private readonly T prefab;
        private readonly int maxSize;

        public ObjectPool(T prefab, int initialSize, Transform parent, int maxSize = 64)
        {
            this.prefab = prefab;
            this.parent = parent;
            this.maxSize = maxSize;

            items = new T[maxSize];
            activeFlags = new bool[maxSize]; // Defaults to false
            poolIndices = new int[maxSize]; // Defaults to 0
            availableIndices = new int[maxSize];

            availableCount = 0;

            // Initialize pool
            Prewarm(initialSize);
        }

        public T Get()
        {
            if (availableCount > 0)
            {
                int index = availableIndices[availableCount - 1];
                availableCount--;

                items[index].gameObject.SetActive(true);
                activeFlags[index] = true;

                return items[index];
            }

            if (availableCount + GetActiveCount() < maxSize)
            {
                int newIndex = GetActiveCount();
                var newItem = CreateNew();

                items[newIndex] = newItem;
                activeFlags[newIndex] = true;
                poolIndices[newIndex] = newIndex;

                return newItem;
            }

            Debug.LogWarning("Pool capacity reached, returning null");
            return null;
        }

        public void Return(T item)
        {
            // Find item index
            for (int i = 0; i < maxSize; i++)
            {
                if (items[i] == item && activeFlags[i])
                {
                    // Deactivate and add to available indices
                    item.gameObject.SetActive(false);
                    activeFlags[i] = false;
                    availableIndices[availableCount] = i;
                    availableCount++;

                    return;
                }
            }
        }

        public void Prewarm(int count)
        {
            int warmCount = Mathf.Min(count, maxSize);

            for (int i = 0; i < warmCount; i++)
            {
                var item = CreateNew();
                item.gameObject.SetActive(false);

                // Store in arrays
                items[i] = item;
                activeFlags[i] = false;
                poolIndices[i] = i;

                // Add to available indices
                availableIndices[availableCount] = i;
                availableCount++;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < maxSize; i++)
            {
                if (items[i] != null)
                {
                    Object.Destroy(items[i].gameObject);
                    items[i] = null;
                    activeFlags[i] = false;
                }
            }

            availableCount = 0;
        }

        //Helper Methods
        private int GetActiveCount()
        {
            int count = 0;
            for (int i = 0; i < maxSize; i++)
            {
                if (items[i] != null && activeFlags[i])
                    count++;
            }
            return count;
        }

        private T CreateNew()
        {
            return Object.Instantiate(prefab, parent);
        }

        // Optional: Get active items without allocation
        public void GetActiveItems(List<T> result)
        {
            result.Clear();
            for (int i = 0; i < maxSize; i++)
            {
                if (items[i] != null && activeFlags[i])
                    result.Add(items[i]);
            }
        }
    }
}
