using System;
using UnityEngine;

public struct EntityGroup<TDataComponentType> : IEntityGroup
    where TDataComponentType : Enum
{
    private ChunkArray<bool> entityStates;
    private IDataComponent[] dataComponents;

    public int EntityCount { get; private set; }

    public EntityGroup(int capacity)
    {
        entityStates = new ChunkArray<bool>(capacity);
        dataComponents = new IDataComponent[Enum.GetValues(typeof(TDataComponentType)).Length];

        EntityCount = 0;
        for (int entityId = 0; entityId < capacity; entityId++)
        {
            entityStates.Add(true);
            EntityCount++;
        }
    }

    public void RegisterComponent<T>(TDataComponentType type, T dataComponent)
        where T : struct, IDataComponent
    {
        dataComponents[Convert.ToInt32(type)] = new DataComponentWrapper<T>(dataComponent);
    }

    public ref T GetComponent<T>(TDataComponentType type)
        where T : struct, IDataComponent
    {
        var index = Convert.ToInt32(type);
        if (index >= dataComponents.Length || dataComponents[index] == null)
        {
            throw new InvalidOperationException($"Component of type {type} is not registered");
        }
        return ref ((DataComponentWrapper<T>)dataComponents[index]).Data;
    }

    public bool IsEntityActive(int entityId)
    {
        return entityId < EntityCount && entityStates.Get(entityId);
    }

    public void SetEntityCount(int count)
    {
        if (count > entityStates.Capacity)
            throw new ArgumentException("Count exceeds capacity");

        EntityCount = count;
        for (int i = 0; i < count; i++)
        {
            entityStates.Set(i, true);
        }
    }

    private class DataComponentWrapper<T> : IDataComponent
        where T : struct
    {
        public T Data;

        public DataComponentWrapper(T data)
        {
            Data = data;
        }
    }
}
