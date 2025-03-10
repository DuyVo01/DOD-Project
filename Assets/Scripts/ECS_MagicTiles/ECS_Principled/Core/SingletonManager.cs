using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// Manages singleton entities that don't benefit from regular chunk storage
    /// </summary>
    public class SingletonManager
    {
        // Map singleton flag types to their chunks
        private Dictionary<Type, SingletonChunk> flagTypeToChunk =
            new Dictionary<Type, SingletonChunk>();

        // Map entity IDs to their chunks for faster lookup
        private Dictionary<int, SingletonChunk> entityIdToChunk =
            new Dictionary<int, SingletonChunk>();

        // Track entity IDs for consistency
        private HashSet<int> managedEntityIds = new HashSet<int>();

        // Starting ID for singletons
        private const int SINGLETON_ID_START = 50000;
        private int nextEntityId = SINGLETON_ID_START;

        // Reference to world for coordination
        private readonly World world;

        public SingletonManager(World world)
        {
            this.world = world;
        }

        /// <summary>
        /// Creates or updates a singleton entity
        /// </summary>
        public int RegisterSingleton(params object[] components)
        {
            // Find the singleton flag component
            Type flagType = null;
            foreach (var component in components)
            {
                if (component is ISingletonFlag)
                {
                    flagType = component.GetType();
                    break;
                }
            }

            if (flagType == null)
            {
                throw new ArgumentException("No ISingletonFlag component found in components");
            }

            // Reuse existing chunk or create a new one
            SingletonChunk chunk;
            int entityId;

            if (flagTypeToChunk.TryGetValue(flagType, out chunk))
            {
                // Update existing chunk
                entityId = chunk.EntityId;

                // Update components
                foreach (var component in components)
                {
                    chunk.SetComponent(component.GetType(), component);
                }

                Debug.Log($"Updated singleton entity {entityId} with flag {flagType.Name}");
            }
            else
            {
                // Create a new entity ID
                entityId = nextEntityId++;
                managedEntityIds.Add(entityId);

                // Create a new chunk
                Archetype archetype = Archetype.FromComponents(components);
                chunk = new SingletonChunk(entityId, archetype, flagType);
                flagTypeToChunk[flagType] = chunk;
                entityIdToChunk[entityId] = chunk;

                // Add components
                foreach (var component in components)
                {
                    chunk.SetComponent(component.GetType(), component);
                }

                Debug.Log($"Created singleton entity {entityId} with flag {flagType.Name}");
            }

            return entityId;
        }

        /// <summary>
        /// Gets a reference to a component from the singleton with the specified flag type
        /// </summary>
        public ref TFlag GetComponent<TFlag>()
            where TFlag : struct, ISingletonFlag, IComponent
        {
            if (!flagTypeToChunk.TryGetValue(typeof(TFlag), out var chunk))
            {
                throw new KeyNotFoundException(
                    $"Singleton of type {typeof(TFlag).Name} not registered"
                );
            }

            return ref chunk.GetComponent<TFlag>();
        }

        /// <summary>
        /// Gets a reference to a singleton component by flag type
        /// </summary>
        public ref T GetComponent<TFlag, T>()
            where TFlag : struct, ISingletonFlag
            where T : struct, IComponent
        {
            if (!flagTypeToChunk.TryGetValue(typeof(TFlag), out var chunk))
            {
                throw new KeyNotFoundException(
                    $"Singleton of type {typeof(TFlag).Name} not registered"
                );
            }

            return ref chunk.GetComponent<T>();
        }

        /// <summary>
        /// Gets a component by entity ID
        /// </summary>
        public ref T GetComponentByEntityId<T>(int entityId)
            where T : struct, IComponent
        {
            if (!entityIdToChunk.TryGetValue(entityId, out var chunk))
            {
                throw new KeyNotFoundException($"Entity {entityId} not found in singleton manager");
            }

            return ref chunk.GetComponent<T>();
        }

        /// <summary>
        /// Gets the entity ID for a singleton by flag type
        /// </summary>
        public int GetEntityId<TFlag>()
            where TFlag : struct, ISingletonFlag
        {
            if (!flagTypeToChunk.TryGetValue(typeof(TFlag), out var chunk))
            {
                throw new KeyNotFoundException(
                    $"Singleton of type {typeof(TFlag).Name} not registered"
                );
            }

            return chunk.EntityId;
        }

        /// <summary>
        /// Checks if an entity is managed by the singleton manager
        /// </summary>
        public bool ManagesEntity(int entityId)
        {
            return managedEntityIds.Contains(entityId);
        }

        /// <summary>
        /// Destroys a singleton entity by flag type
        /// </summary>
        public void DestroySingleton<TFlag>()
            where TFlag : struct, ISingletonFlag
        {
            if (flagTypeToChunk.TryGetValue(typeof(TFlag), out var chunk))
            {
                managedEntityIds.Remove(chunk.EntityId);
                entityIdToChunk.Remove(chunk.EntityId);
                flagTypeToChunk.Remove(typeof(TFlag));

                Debug.Log($"Destroyed singleton entity with flag {typeof(TFlag).Name}");
            }
        }

        /// <summary>
        /// Destroys a singleton entity by ID
        /// </summary>
        public void DestroyEntityById(int entityId)
        {
            if (!entityIdToChunk.TryGetValue(entityId, out var chunk))
            {
                return;
            }

            managedEntityIds.Remove(entityId);
            entityIdToChunk.Remove(entityId);
            flagTypeToChunk.Remove(chunk.FlagType);

            Debug.Log($"Destroyed singleton entity {entityId}");
        }

        /// <summary>
        /// Gets all singleton entity IDs
        /// </summary>
        public IEnumerable<int> GetAllEntityIds()
        {
            return managedEntityIds;
        }

        /// <summary>
        /// Gets all singleton flag types
        /// </summary>
        public IEnumerable<Type> GetAllFlagTypes()
        {
            return flagTypeToChunk.Keys;
        }
    }
}
