using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Core
{
    public interface IGameSystem
    {
        void Initialize();
        void Start();
        void Update();
        void Cleanup();
        bool AutoUpdate { get; set; }
        ArchetypeManager ArchetypeManager { get; set; }
    }

    public static class SystemManager
    {
        private static Dictionary<Type, IGameSystem> systems = new();
        private static bool isInitialized;

        public static void RegisterSystem<T>(T system)
            where T : struct, IGameSystem
        {
            if (systems.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"System {typeof(T)} is already registered. Skipping.");
                return;
            }

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

        public static void InitializeSystems(World world)
        {
            if (isInitialized)
            {
                Debug.LogWarning("Systems are already initialized.");
                return;
            }

            foreach (var system in systems.Values)
            {
                try
                {
                    system.ArchetypeManager = world.ArchetypeManager;
                    system.Initialize();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to initialize system {system.GetType()}: {e}");
                }
            }

            isInitialized = true;
        }

        public static void SystemStart()
        {
            if (!isInitialized)
            {
                Debug.LogError("Systems not initialized. Call InitializeSystems first.");
                return;
            }
            foreach (var system in systems.Values)
            {
                // Only update systems that are enabled AND marked for auto-update
                if (((ISystemState)system).Enabled)
                {
                    try
                    {
                        system.Start();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error Starting system {system.GetType()}: {e}");
                    }
                }
            }
        }

        public static void UpdateSystems()
        {
            if (!isInitialized)
            {
                Debug.LogError("Systems not initialized. Call InitializeSystems first.");
                return;
            }

            foreach (var system in systems.Values)
            {
                // Only update systems that are enabled AND marked for auto-update
                if (((ISystemState)system).Enabled && system.AutoUpdate)
                {
                    try
                    {
                        system.Update();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error updating system {system.GetType()}: {e}");
                    }
                }
            }
        }

        public static void EnableSystem<T>()
            where T : struct, IGameSystem
        {
            if (systems.TryGetValue(typeof(T), out var system))
            {
                ((ISystemState)system).Enabled = true;
            }
        }

        public static void DisableSystem<T>()
            where T : struct, IGameSystem
        {
            if (systems.TryGetValue(typeof(T), out var system))
            {
                ((ISystemState)system).Enabled = false;
            }
        }

        public static void Clear()
        {
            if (!isInitialized)
                return;

            foreach (var system in systems.Values)
            {
                try
                {
                    system.Cleanup();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error cleaning up system {system.GetType()}: {e}");
                }
            }

            systems.Clear();
            isInitialized = false;
        }

        private interface ISystemState
        {
            bool Enabled { get; set; }
        }

        private class GameSystemWrapper<T> : IGameSystem, ISystemState
            where T : IGameSystem
        {
            public T System;
            public bool Enabled { get; set; } = true;
            public bool AutoUpdate
            {
                get => System.AutoUpdate;
                set => System.AutoUpdate = value;
            }
            public ArchetypeManager ArchetypeManager
            {
                get => System.ArchetypeManager;
                set => System.ArchetypeManager = value;
            }

            public GameSystemWrapper(T system)
            {
                System = system;
            }

            public void Initialize() => System.Initialize();

            public void Update() => System.Update();

            public void Cleanup() => System.Cleanup();

            public void Start() => System.Start();
        }
    }
}

// Example usage:
