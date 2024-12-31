using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

            Array.Fill(sparse, -1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(int entityId, in T value)
        {
            if (entityId >= sparse.Length)
            {
                int newCapacity = Mathf.Max(sparse.Length * 2, entityId + 1);
                Array.Resize(ref sparse, newCapacity);

                Array.Fill(sparse, -1, sparse.Length / 2, newCapacity - sparse.Length / 2);
            }

            if (count >= dense.Length)
            {
                int newCapacity = (int)(dense.Length * GROWTH_FACTOR);
                Array.Resize(ref dense, newCapacity);
                Array.Resize(ref data, newCapacity);
            }

            // Add to sparse set
            sparse[entityId] = count;
            dense[count] = entityId;
            data[count] = value;
            count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get(int entityId)
        {
            if (!Contains(entityId))
            {
                throw new ArgumentException($"Entity {entityId} not found in sparse set");
            }

            return ref data[sparse[entityId]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int entityId)
        {
            return entityId < sparse.Length && sparse[entityId] != -1 && sparse[entityId] < count;
        }

        // Efficient iteration methods
        public Span<T> GetDataSpan() => new Span<T>(data, 0, count);

        public Span<int> GetEntitySpan() => new Span<int>(dense, 0, count);

        // New method to get dense array of entity IDs
        public ReadOnlySpan<int> GetEntityIds()
        {
            return new ReadOnlySpan<int>(dense, 0, count);
        }

        public int GetEntityIndex(int entityId)
        {
            return Contains(entityId) ? sparse[entityId] : -1;
        }

        public void Clear()
        {
            Array.Fill(sparse, -1);
            count = 0;
        }

        public int Count => count;
    }
}
