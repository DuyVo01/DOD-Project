using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    // Our central system management class
    public static class SystemRegistry
    {
        // Store systems in order of execution priority
        private static readonly List<IGameSystem> updateSystems = new();
        private static World world;
        private static bool isInitialized;
        private static EGameState currentGameState = EGameState.IngamePrestart;

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
            system.RunInitialize();

            updateSystems.Add(system);

            Debug.Log($"Added system: {system.GetType().Name}");
        }

        public static void Update(float deltaTime)
        {
            if (!isInitialized)
                return;

            foreach (var system in updateSystems)
            {
                if (
                    system.IsEnabled
                    && (
                        system.GameStateToExecute == currentGameState
                        || system.GameStateToExecute == EGameState.All
                    )
                )
                {
                    try
                    {
                        system.RunUpdate(deltaTime);
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
                system.RunCleanup();
            }

            updateSystems.Clear();
            isInitialized = false;
            world = null;
        }

        public static void SetGameState(EGameState newState)
        {
            currentGameState = newState;
        }
    }
}
