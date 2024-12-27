using System;
using UnityEngine;

public class NativePool<T> : INativePool<T>
    where T : class
{
    private readonly T[] items;
    private readonly bool[] activeFlags;
    private readonly int[] availableIndices;
    private readonly Func<T> createFunc;
    private readonly Action<T> resetFunc;

    private int availableCount;
    private readonly int maxSize;

    public NativePool(
        Func<T> createFunc,
        Action<T> resetFunc = null,
        int initialSize = 16,
        int maxSize = 64
    )
    {
        this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
        this.resetFunc = resetFunc;
        this.maxSize = maxSize;

        items = new T[maxSize];
        activeFlags = new bool[maxSize];
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

            activeFlags[index] = true;
            return items[index];
        }

        if (GetActiveCount() < maxSize)
        {
            int newIndex = GetActiveCount();
            var newItem = createFunc();

            items[newIndex] = newItem;
            activeFlags[newIndex] = true;

            return newItem;
        }

        Debug.LogWarning($"Native pool capacity reached for type {typeof(T).Name}, returning null");
        return null;
    }

    public void Return(T item)
    {
        for (int i = 0; i < maxSize; i++)
        {
            if (ReferenceEquals(items[i], item) && activeFlags[i])
            {
                activeFlags[i] = false;
                availableIndices[availableCount] = i;
                availableCount++;

                // Reset the item if a reset function was provided
                resetFunc?.Invoke(item);
                return;
            }
        }
    }

    private void Prewarm(int count)
    {
        int warmCount = Mathf.Min(count, maxSize);

        for (int i = 0; i < warmCount; i++)
        {
            var item = createFunc();

            items[i] = item;
            activeFlags[i] = false;

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
                // If item implements IDisposable, dispose it
                if (items[i] is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                items[i] = null;
                activeFlags[i] = false;
            }
        }
        availableCount = 0;
    }

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
}
