using System;
using System.Collections.Generic;

public class ChunkArray<T>
{
    private readonly int chunkSize;
    private T[][] chunks;
    private int count;
    private int capacity;
    private readonly Stack<(int, int)> freeIndices;

    public int Count => count;
    public int Capacity => capacity;

    public ChunkArray(int initialCapacity, int chunkSize = 128)
    {
        if (initialCapacity < 0)
        {
            throw new ArgumentException(
                "Initial capacity must be non-negative",
                nameof(initialCapacity)
            );
        }
        if (chunkSize <= 0)
        {
            throw new ArgumentException("Chunk size must be positive", nameof(chunkSize));
        }

        this.chunkSize = chunkSize;
        int initialChunks = (initialCapacity + chunkSize - 1) / chunkSize;
        chunks = new T[initialChunks][];
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i] = new T[chunkSize];
        }

        capacity = initialChunks * chunkSize;
        freeIndices = new Stack<(int, int)>();
    }

    public void Add(T item)
    {
        int chunkIndex,
            elementIndex;

        if (freeIndices.Count > 0)
        {
            (chunkIndex, elementIndex) = freeIndices.Pop();
        }
        else
        {
            if (count == capacity)
            {
                GrowChunks();
            }

            chunkIndex = count / chunkSize;
            elementIndex = count % chunkSize;
        }

        chunks[chunkIndex][elementIndex] = item;
        count++;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }

        int chunkIndex = index / chunkSize;
        int elementIndex = index % chunkSize;

        chunks[chunkIndex][elementIndex] = default;
        freeIndices.Push((chunkIndex, elementIndex));
        count--;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        int chunkIndex = index / chunkSize;
        int elementIndex = index % chunkSize;
        return chunks[chunkIndex][elementIndex];
    }

    public void Set(int index, T value)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));

        int chunkIndex = index / chunkSize;
        int elementIndex = index % chunkSize;
        chunks[chunkIndex][elementIndex] = value;
    }

    //Private Helper Methods
    private void GrowChunks()
    {
        int newChunkIndex = chunks.Length;
        Array.Resize(ref chunks, newChunkIndex + 1);
        chunks[newChunkIndex] = new T[chunkSize];
        capacity += chunkSize;
    }

    public void Clear()
    {
        count = 0;
        freeIndices.Clear();

        // Optional: Clear array contents
        for (int i = 0; i < chunks.Length; i++)
        {
            Array.Clear(chunks[i], 0, chunks[i].Length);
        }
    }
}
