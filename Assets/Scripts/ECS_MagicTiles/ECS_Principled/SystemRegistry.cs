using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public interface IGameSystem
    {
        void SetWorld(World world);
        void Initialize(); // Called when system is first created
        void Update(float deltaTime); // Called every frame
        void Cleanup();
        bool IsEnabled { get; set; } // Controls if system should be updated
        World World { get; set; }
    }

    // Our central system management class
    public static class SystemRegistry
    {
        // Interface that all our game systems will implement


        // Store systems in order of execution priority
        private static readonly List<IGameSystem> updateSystems = new();
        private static World world;
        private static bool isInitialized;

        // Initialize our registry with a world reference
        public static void Initialize(World gameWorld)
        {
            // Prevent multiple initializations
            if (isInitialized)
            {
                Debug.LogWarning("SystemRegistry already initialized!");
                return;
            }

            world = gameWorld;
            isInitialized = true;
        }

        // Allow adding systems from GlobalPoint
        public static void AddSystem(IGameSystem system)
        {
            if (!isInitialized)
            {
                Debug.LogError("SystemRegistry not initialized! Call Initialize first.");
                return;
            }

            // Provide the system with world reference and initialize it
            system.SetWorld(world);
            system.Initialize();

            updateSystems.Add(system);

            Debug.Log($"Added system: {system.GetType().Name}");
        }

        public static void Update(float deltaTime)
        {
            if (!isInitialized)
                return;

            foreach (var system in updateSystems)
            {
                if (system.IsEnabled)
                {
                    try
                    {
                        system.Update(deltaTime);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error in system {system.GetType().Name}: {e}");
                    }
                }
            }
        }

        public static void Cleanup()
        {
            foreach (var system in updateSystems)
            {
                system.Cleanup();
            }

            updateSystems.Clear();
            isInitialized = false;
            world = null;
        }
    }
}
