using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// The main ECS world that manages entities, components, and systems
    /// </summary>
    public class World
    {
        // Chunk-based storage system
        private readonly ChunkManager chunkManager;

        // Next available entity ID
        private int nextEntityId = 1000; // Starting from 1000 to leave room for special IDs

        // Track active entities
        private HashSet<int> activeEntities;

        // System registry
        private readonly Dictionary<Type, IGameSystem> systems;

        // Singleton entity manager
        private readonly SingletonManager singletonManager;

        /// <summary>
        /// Provides access to the singleton manager
        /// </summary>
        public SingletonManager Singletons => singletonManager;

        /// <summary>
        /// Creates a new ECS world
        /// </summary>
        public World()
        {
            chunkManager = new ChunkManager();
            activeEntities = new HashSet<int>();
            systems = new Dictionary<Type, IGameSystem>();
            singletonManager = new SingletonManager(this);
        }

        /// <summary>
        /// Generate a new unique entity ID
        /// </summary>
        private int CreateEntityId()
        {
            int entityId = nextEntityId++;
            activeEntities.Add(entityId);
            return entityId;
        }

        /// <summary>
        /// Create an entity with dynamic archetype detection from components
        /// </summary>
        public int CreateEntity(params object[] components)
        {
            // Check if this entity should be a singleton
            foreach (var component in components)
            {
                if (component is ISingletonFlag)
                {
                    // Route to singleton manager instead
                    return singletonManager.RegisterSingleton(components);
                }
            }

            // Extract component types and create or get archetype
            Archetype archetype = Archetype.FromComponents(components);

            // Create entity with this archetype
            int entityId = CreateEntityId();
            chunkManager.AddEntity(entityId, archetype, components);

            return entityId;
        }

        /// <summary>
        /// Create an entity with a specific archetype
        /// </summary>
        public int CreateEntityWithArchetype(Archetype archetype, object[] components)
        {
            // Check if this entity should be a singleton
            foreach (var component in components)
            {
                if (component is ISingletonFlag)
                {
                    // Route to singleton manager instead
                    return singletonManager.RegisterSingleton(components);
                }
            }

            int entityId = CreateEntityId();
            chunkManager.AddEntity(entityId, archetype, components);
            return entityId;
        }

        /// <summary>
        /// Legacy method for compatibility with existing code
        /// </summary>
        public int CreateEntityWithComponents(Archetype archetype, object[] components)
        {
            return CreateEntityWithArchetype(archetype, components);
        }

        /// <summary>
        /// Get a component for a specific entity
        /// </summary>
        public ref T GetComponent<T>(int entityId)
            where T : struct, IComponent
        {
            // Check if this is a singleton entity
            if (singletonManager.ManagesEntity(entityId))
            {
                return ref singletonManager.GetComponentByEntityId<T>(entityId);
            }

            return ref chunkManager.GetComponentRef<T>(entityId);
        }

        /// <summary>
        /// Create a query for processing entities
        /// </summary>
        public EntityQuery CreateQuery()
        {
            return new EntityQuery(chunkManager);
        }

        /// <summary>
        /// Check if an entity exists
        /// </summary>
        public bool EntityExists(int entityId)
        {
            return activeEntities.Contains(entityId) || singletonManager.ManagesEntity(entityId);
        }

        /// <summary>
        /// Destroy an entity and remove all its components
        /// </summary>
        public void DestroyEntity(int entityId)
        {
            // Check if this is a singleton entity
            if (singletonManager.ManagesEntity(entityId))
            {
                singletonManager.DestroyEntityById(entityId);
                return;
            }

            if (!activeEntities.Contains(entityId))
            {
                return;
            }

            chunkManager.RemoveEntity(entityId);
            activeEntities.Remove(entityId);
        }

        /// <summary>
        /// Register a system to be executed by this world
        /// </summary>
        public void RegisterSystem<T>(T system)
            where T : IGameSystem
        {
            Type systemType = typeof(T);

            if (systems.ContainsKey(systemType))
            {
                Debug.LogWarning($"System of type {systemType.Name} already registered");
                return;
            }

            systems[systemType] = system;
            system.SetWorld(this);
            system.RunInitialize();
        }

        /// <summary>
        /// Get a registered system by type
        /// </summary>
        public T GetSystem<T>()
            where T : IGameSystem
        {
            Type systemType = typeof(T);

            if (!systems.TryGetValue(systemType, out var system))
            {
                throw new KeyNotFoundException($"System of type {systemType.Name} not found");
            }

            return (T)system;
        }

        /// <summary>
        /// Execute all registered systems
        /// </summary>
        public void Update(float deltaTime)
        {
            foreach (var system in systems.Values)
            {
                if (system.IsEnabled)
                {
                    system.RunUpdate(deltaTime);
                }
            }
        }

        /// <summary>
        /// Clean up all systems
        /// </summary>
        public void Cleanup()
        {
            foreach (var system in systems.Values)
            {
                system.RunCleanup();
            }
        }

        /// <summary>
        /// Get a reference to a singleton component by its flag type
        /// </summary>
        public ref T GetSingleton<T>()
            where T : struct, ISingletonFlag, IComponent
        {
            return ref singletonManager.GetComponent<T>();
        }

        /// <summary>
        /// Get a reference to a singleton component by flag type
        /// </summary>
        public ref T GetSingleton<TFlag, T>()
            where TFlag : struct, ISingletonFlag
            where T : struct, IComponent
        {
            return ref singletonManager.GetComponent<TFlag, T>();
        }

        /// <summary>
        /// Get singleton entity ID by flag type
        /// </summary>
        public int GetSingletonEntityId<TFlag>()
            where TFlag : struct, ISingletonFlag
        {
            return singletonManager.GetEntityId<TFlag>();
        }

        /// <summary>
        /// Check if a singleton exists by flag type
        /// </summary>
        public bool SingletonExists<TFlag>()
            where TFlag : struct, ISingletonFlag
        {
            return singletonManager.GetAllFlagTypes().Contains(typeof(TFlag));
        }

        // For backward compatibility with existing systems
        // This will be deprecated in the future
        public ArchetypeStorage GetStorage(Archetype archetype)
        {
            Debug.LogWarning(
                "GetStorage is deprecated. Use CreateQuery() instead for better performance."
            );

            // Create a legacy storage adapter that wraps our chunk-based storage
            return new ArchetypeStorageAdapter(archetype, chunkManager);
        }

        // Adapter class to maintain compatibility with old code
        private class ArchetypeStorageAdapter : ArchetypeStorage
        {
            private readonly Archetype archetype;
            private readonly ChunkManager chunkManager;

            public ArchetypeStorageAdapter(Archetype archetype, ChunkManager chunkManager)
                : base(archetype, 0) // The capacity won't be used
            {
                this.archetype = archetype;
                this.chunkManager = chunkManager;
            }

            // Override methods to delegate to chunk manager
            public new T[] GetComponents<T>()
                where T : struct
            {
                var chunks = chunkManager.GetChunksWithComponent<T>();

                // Count total entities
                int totalCount = 0;
                foreach (var chunk in chunks)
                {
                    totalCount += chunk.Count;
                }

                // Create combined array
                T[] result = new T[totalCount];
                int currentIndex = 0;

                // Copy data from all chunks
                foreach (var chunk in chunks)
                {
                    var compArray = chunk.GetComponentArray<T>();
                    Array.Copy(compArray, 0, result, currentIndex, chunk.Count);
                    currentIndex += chunk.Count;
                }

                return result;
            }

            // Other overrides would go here as needed
        }
    }
}
