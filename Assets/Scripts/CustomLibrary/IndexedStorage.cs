using System;
using System.Collections.Generic;

public class IndexedStorage<T>
{
    private T[] items;
    private Dictionary<int, int> idToIndex;
    private int[] indexToId;
    private int count;

    public int Count => count;

    public IndexedStorage(int capacity)
    {
        items = new T[capacity];
        idToIndex = new Dictionary<int, int>(capacity);
        indexToId = new int[capacity];
        count = 0;
    }

    public void Add(int id, T item)
    {
        // Resize array if needed
        if (count >= items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
            Array.Resize(ref indexToId, items.Length);
        }

        // Store item at the end of the array
        int index = count;
        items[index] = item;

        // Map id to array index
        idToIndex[id] = index;
        indexToId[index] = id;

        count++;
    }

    public void Remove(int id)
    {
        if (idToIndex.TryGetValue(id, out int indexToRemove))
        {
            // Get the last element's index
            int lastIndex = count - 1;

            // Get the ID of the last element directly
            int lastItemId = indexToId[lastIndex];

            // Move the last element to fill the gap (if not removing the last element)
            if (indexToRemove != lastIndex)
            {
                items[indexToRemove] = items[lastIndex];
                idToIndex[lastItemId] = indexToRemove;
                indexToId[indexToRemove] = lastItemId;
            }

            // Clear the last slot and remove the ID mapping
            items[lastIndex] = default;
            idToIndex.Remove(id);
            count--;
        }
    }

    public T GetById(int id)
    {
        if (idToIndex.TryGetValue(id, out int index))
        {
            return items[index];
        }
        throw new KeyNotFoundException($"Item with id {id} not found");
    }

    public bool TryGetById(int id, out T item)
    {
        if (idToIndex.TryGetValue(id, out int index))
        {
            item = items[index];
            return true;
        }
        item = default;
        return false;
    }

    public Span<T> AsSpan()
    {
        return new Span<T>(items, 0, count);
    }

    // Optional: Direct access to an item by its array index (not ID)
    // Use with caution as indices can change when items are removed
    public T GetByIndex(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();
        return items[index];
    }

    // Iterate over all items with their IDs
    public void ForEach(Action<int, T> action)
    {
        for (int i = 0; i < count; i++)
        {
            action(indexToId[i], items[i]);
        }
    }

    // Clear the collection
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            items[i] = default;
        }
        idToIndex.Clear();
        count = 0;
    }

    // Check if an ID exists in the collection
    public bool Contains(int id)
    {
        return idToIndex.ContainsKey(id);
    }

    // Trim excess capacity
    public void TrimExcess()
    {
        if (count < items.Length / 2)
        {
            int newCapacity = Math.Max(count, 16); // Don't go below reasonable minimum
            Array.Resize(ref items, newCapacity);
            Array.Resize(ref indexToId, newCapacity);
        }
    }
}
