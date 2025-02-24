using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemRepository
{
    private static Dictionary<Type, IGameSystem> systems = new System.Collections.Generic.Dictionary<System.Type, IGameSystem>();

    public static void RegisterSystem<T>(T system)
        where T : struct, IGameSystem
    {
        systems[typeof(T)] = new GameSystemWrapper<T>(system);
    }

    public static ref T GetSystem<T>()
        where T : struct, IGameSystem
    {
        if (systems.TryGetValue(typeof(T), out var wrapper))
        {
            return ref ((GameSystemWrapper<T>)wrapper).System;
        }
        throw new KeyNotFoundException($"System {typeof(T)} not registered");
    }

    public static void Clear()
    {
        systems.Clear();
    }

    private class GameSystemWrapper<T> : IGameSystem
        where T : IGameSystem
    {
        public T System;

        public GameSystemWrapper(T system)
        {
            System = system;
        }
    }
}
