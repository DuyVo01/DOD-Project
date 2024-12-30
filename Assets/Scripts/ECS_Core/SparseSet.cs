using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Core
{
    public class SparseSet<T>
    {
        private const int DEFAULT_CAPACITY = 64;
        private const float GROWTH_FACTOR = 2.0f;

        private int[] sparse;
        private int[] dense;
        private T[] data;
        private int count;

        public SparseSet(int initialCapacity = DEFAULT_CAPACITY)
        {
            sparse = new int[initialCapacity];
            dense = new int[initialCapacity];
            data = new T[initialCapacity];
            count = 0;

            for (int i = 0; i < initialCapacity; i++)
            {
                sparse[i] = -1;
            }
        }

        public void Add(int entityId, in T value)
        {
            if (entityId >= sparse.Length)
            {
                GrowSparse(entityId);
            }

            if (entityId >= dense.Length)
            {
                GrowDense();
            }

            // Add to sparse set
            sparse[entityId] = count;
            dense[count] = entityId;
            data[count] = value;
            count++;
        }

        public void Remove(int entityId)
        {
            if (!Contains(entityId))
            {
                return;
            }

            int denseIndex = sparse[entityId];
            int lastEntityId = dense[count - 1];

            if (denseIndex < count - 1)
            {
                data[denseIndex] = data[count - 1];
                dense[denseIndex] = lastEntityId;
                sparse[lastEntityId] = denseIndex;
            }

            sparse[entityId] = -1;
            count--;
        }

        public bool TryGet(int entityId, out T value)
        {
            if (Contains(entityId))
            {
                value = data[sparse[entityId]];
                return true;
            }
            value = default;
            return false;
        }

        public void Set(int entityId, in T value)
        {
            if (Contains(entityId))
            {
                data[sparse[entityId]] = value;
            }
        }

        private void GrowSparse(int minCapacity)
        {
            int newCapacity = Mathf.Max(sparse.Length * 2, minCapacity + 1);
            Array.Resize(ref sparse, newCapacity);

            // Initialize new elements with invalid indices
            for (int i = sparse.Length / 2; i < newCapacity; i++)
            {
                sparse[i] = -1;
            }
        }

        private void GrowDense()
        {
            int newCapacity = (int)(dense.Length * GROWTH_FACTOR);
            Array.Resize(ref dense, newCapacity);
            Array.Resize(ref data, newCapacity);
        }

        public bool Contains(int entityId)
        {
            return entityId < sparse.Length && sparse[entityId] != -1 && sparse[entityId] < count;
        }

        // Efficient iteration methods
        public Span<T> GetDataSpan() => new Span<T>(data, 0, count);

        public Span<int> GetEntitySpan() => new Span<int>(dense, 0, count);

        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                sparse[dense[i]] = -1;
            }
            count = 0;
        }

        public int Count => count;
    }
}
