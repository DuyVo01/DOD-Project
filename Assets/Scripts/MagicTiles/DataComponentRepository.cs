using System;
using System.Collections.Generic;

public static class DataComponentRepository
{
    private static Dictionary<Type, IDataComponent> dataComponents = new();

    public static void RegisterData<T>(T dataComponent)
        where T : struct, IDataComponent
    {
        dataComponents[typeof(T)] = new DataComponentWrapper<T>(dataComponent);
    }

    public static ref T GetData<T>()
        where T : struct, IDataComponent
    {
        if (dataComponents.TryGetValue(typeof(T), out var wraper))
        {
            return ref ((DataComponentWrapper<T>)wraper).Data;
        }
        throw new KeyNotFoundException($"Data component {typeof(T)} not registered");
    }

    public static void Clear()
    {
        dataComponents.Clear();
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
