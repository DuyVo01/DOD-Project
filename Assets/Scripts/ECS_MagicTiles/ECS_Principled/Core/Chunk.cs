using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    /// <summary>
    /// Represents a chunk of memory storing components for entities of the same archetype
    /// </summary>
    public class Chunk
    {
        // Default chunk capacity (entities per chunk)
        public const int DEFAULT_CAPACITY = 128;

        // The archetype of entities in this chunk
        public Archetype Archetype { get; }

        // Number of entities currently in this chunk
        public int Count { get; private set; }

        // Maximum number of entities this chunk can hold
        public int Capacity { get; }

        // Stores entity IDs with fast O(1) lookups
        private IndexedStorage<int> entityIds;

        // Typed arrays for storing component data
        private Dictionary<ComponentType, Array> componentArrays;

        // Tracks the next index to use for entity storage
        private int nextAvailableIndex;

        // Tracks available slots for reuse when entities are removed
        private Stack<int> freeIndices;

        /// <summary>
        /// Creates a new chunk for storing entities of a specific archetype
        /// </summary>
        public Chunk(Archetype archetype, int capacity = DEFAULT_CAPACITY)
        {
            Archetype = archetype;
            Capacity = capacity;
            Count = 0;
            nextAvailableIndex = 0;

            // Initialize entity tracking
            entityIds = new IndexedStorage<int>(capacity);
            freeIndices = new Stack<int>();

            // Create component arrays for each component type
            componentArrays = new Dictionary<ComponentType, Array>();
            foreach (var componentType in archetype.GetTypes())
            {
                componentArrays[componentType] = Array.CreateInstance(componentType.Type, capacity);
            }
        }

        /// <summary>
        /// Adds an entity with its components to this chunk
        /// </summary>
        public bool TryAddEntity(int entityId, object[] components)
        {
            // Verify input
            if (components.Length != Archetype.GetTypes().Length)
            {
                Debug.LogError(
                    $"Component count mismatch: got {components.Length}, expected {Archetype.GetTypes().Length}"
                );
                return false;
            }

            // Check if chunk is full
            if (Count >= Capacity)
            {
                return false;
            }

            // Get the index to use (reuse free slots when available)
            int index;
            if (freeIndices.Count > 0)
            {
                index = freeIndices.Pop();
            }
            else
            {
                index = nextAvailableIndex++;
            }

            // Add the entity ID
            entityIds.Add(entityId, index);

            // Add components to their respective arrays
            var componentTypes = Archetype.GetTypes();
            for (int i = 0; i < componentTypes.Length; i++)
            {
                componentArrays[componentTypes[i]].SetValue(components[i], index);
            }

            // Increment entity count
            Count++;

            return true;
        }

        /// <summary>
        /// Gets a typed component array for iteration
        /// </summary>
        public T[] GetComponentArray<T>()
            where T : struct
        {
            var componentType = ComponentType.Registry.GetComponentType<T>();
            if (!componentArrays.TryGetValue(componentType, out var array))
            {
                throw new InvalidOperationException(
                    $"Component type {typeof(T).Name} not found in chunk"
                );
            }

            return (T[])array;
        }

        /// <summary>
        /// Gets a direct reference to a component for an entity
        /// </summary>
        public ref T GetComponentRef<T>(int entityId)
            where T : struct
        {
            if (!entityIds.TryGetById(entityId, out int index))
            {
                throw new KeyNotFoundException($"Entity {entityId} not found in chunk");
            }

            var componentType = ComponentType.Registry.GetComponentType<T>();
            if (!componentArrays.TryGetValue(componentType, out var array))
            {
                throw new InvalidOperationException(
                    $"Component type {typeof(T).Name} not found in chunk"
                );
            }

            return ref ((T[])array)[index];
        }

        /// <summary>
        /// Gets a direct reference to a component at a specific index
        /// </summary>
        public ref T GetComponentRefByIndex<T>(int index)
            where T : struct
        {
            if (index < 0 || index >= nextAvailableIndex)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }

            var componentType = ComponentType.Registry.GetComponentType<T>();
            if (!componentArrays.TryGetValue(componentType, out var array))
            {
                throw new InvalidOperationException(
                    $"Component type {typeof(T).Name} not found in chunk"
                );
            }

            return ref ((T[])array)[index];
        }

        /// <summary>
        /// Removes an entity from the chunk
        /// </summary>
        public bool RemoveEntity(int entityId)
        {
            // Find the entity's index
            if (!entityIds.TryGetById(entityId, out int index))
            {
                return false;
            }

            // Remove the entity ID
            entityIds.Remove(entityId);

            // Mark the index as available for reuse
            freeIndices.Push(index);

            // Decrement entity count
            Count--;

            return true;
        }

        /// <summary>
        /// Checks if the chunk contains an entity
        /// </summary>
        public bool ContainsEntity(int entityId)
        {
            return entityIds.Contains(entityId);
        }

        /// <summary>
        /// Gets the entity ID at a specific index
        /// </summary>
        public int GetEntityIdByIndex(int index)
        {
            return entityIds.GetByIndex(index);
        }

        /// <summary>
        /// Gets all entity IDs in this chunk as an array
        /// </summary>
        public int[] GetEntityIds()
        {
            var span = entityIds.AsSpan();
            int[] result = new int[span.Length];
            for (int i = 0; i < span.Length; i++)
            {
                result[i] = span[i];
            }
            return result;
        }

        /// <summary>
        /// Returns true if the chunk is full
        /// </summary>
        public bool IsFull => Count >= Capacity;

        /// <summary>
        /// Returns true if the chunk has no entities
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Process entities with a single component type using cache-friendly blocks
        /// </summary>
        public void ProcessInBlocks<T>(ActionRefWithEntity<T> action, int blockSize = 64)
            where T : struct
        {
            var componentArray = GetComponentArray<T>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Process each entity in the block
                for (int i = 0; i < currentBlockSize; i++)
                {
                    int index = blockStart + i;
                    int entityId = ids[index];

                    // Call action with direct ref to component
                    action(ref componentArray[index], entityId);
                }
            }
        }

        /// <summary>
        /// Process entities with two component types using cache-friendly blocks
        /// </summary>
        public void ProcessInBlocks<T1, T2>(ActionRefWithEntity<T1, T2> action, int blockSize = 64)
            where T1 : struct
            where T2 : struct
        {
            var components1 = GetComponentArray<T1>();
            var components2 = GetComponentArray<T2>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Process each entity in the block
                for (int i = 0; i < currentBlockSize; i++)
                {
                    int index = blockStart + i;
                    int entityId = ids[index];

                    // Call action with direct refs to components
                    action(ref components1[index], ref components2[index], entityId);
                }
            }
        }

        /// <summary>
        /// Process entities with three component types using cache-friendly blocks
        /// </summary>
        public void ProcessInBlocks<T1, T2, T3>(
            ActionRefWithEntity<T1, T2, T3> action,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var components1 = GetComponentArray<T1>();
            var components2 = GetComponentArray<T2>();
            var components3 = GetComponentArray<T3>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Process each entity in the block
                for (int i = 0; i < currentBlockSize; i++)
                {
                    int index = blockStart + i;
                    int entityId = ids[index];

                    // Call action with direct refs to components
                    action(
                        ref components1[index],
                        ref components2[index],
                        ref components3[index],
                        entityId
                    );
                }
            }
        }

        /// <summary>
        /// Process entities with block access to component arrays
        /// </summary>
        public void ProcessBlock<T>(BlockAction<T> blockAction, int blockSize = 64)
            where T : struct
        {
            var componentArray = GetComponentArray<T>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Call the block action with array slice information
                blockAction(componentArray, ids, blockStart, currentBlockSize);
            }
        }

        /// <summary>
        /// Process entities with block access to component arrays
        /// </summary>
        public void ProcessBlock<T1, T2>(BlockAction<T1, T2> blockAction, int blockSize = 64)
            where T1 : struct
            where T2 : struct
        {
            var components1 = GetComponentArray<T1>();
            var components2 = GetComponentArray<T2>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Call the block action with array slice information
                blockAction(components1, components2, ids, blockStart, currentBlockSize);
            }
        }

        /// <summary>
        /// Process entities with block access to component arrays
        /// </summary>
        public void ProcessBlock<T1, T2, T3>(
            BlockAction<T1, T2, T3> blockAction,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var components1 = GetComponentArray<T1>();
            var components2 = GetComponentArray<T2>();
            var components3 = GetComponentArray<T3>();
            var ids = GetEntityIds();

            for (int blockStart = 0; blockStart < Count; blockStart += blockSize)
            {
                int currentBlockSize = Math.Min(blockSize, Count - blockStart);

                // Call the block action with array slice information
                blockAction(
                    components1,
                    components2,
                    components3,
                    ids,
                    blockStart,
                    currentBlockSize
                );
            }
        }
    }

    /// <summary>
    /// Manages chunks and provides entity and component access
    /// </summary>
    public class ChunkManager
    {
        // Maps archetype hashes to lists of chunks
        private Dictionary<int, List<Chunk>> archetypeToChunks;

        // Maps component types to chunks containing that component
        private Dictionary<ComponentType, HashSet<Chunk>> componentToChunks;

        // Maps entity IDs to their location (chunk and index)
        private Dictionary<int, (Chunk chunk, int entityId)> entityLocations;

        // Exposes the component-to-chunks mapping for extensions
        public IReadOnlyDictionary<ComponentType, HashSet<Chunk>> ComponentToChunks =>
            componentToChunks;

        /// <summary>
        /// Creates a new chunk manager
        /// </summary>
        public ChunkManager()
        {
            archetypeToChunks = new Dictionary<int, List<Chunk>>();
            componentToChunks = new Dictionary<ComponentType, HashSet<Chunk>>();
            entityLocations = new Dictionary<int, (Chunk, int)>();
        }

        /// <summary>
        /// Adds an entity with its components to the system
        /// </summary>
        public void AddEntity(int entityId, Archetype archetype, object[] components)
        {
            // Get or create the list of chunks for this archetype
            List<Chunk> chunks;
            if (!archetypeToChunks.TryGetValue(archetype.GetHash(), out chunks))
            {
                chunks = new List<Chunk>();
                archetypeToChunks[archetype.GetHash()] = chunks;

                // Register this archetype with its component types
                foreach (var componentType in archetype.GetTypes())
                {
                    if (!componentToChunks.TryGetValue(componentType, out var componentChunks))
                    {
                        componentChunks = new HashSet<Chunk>();
                        componentToChunks[componentType] = componentChunks;
                    }
                }
            }

            // Find a chunk with space or create a new one
            Chunk targetChunk = null;
            foreach (var chunk in chunks)
            {
                if (!chunk.IsFull)
                {
                    targetChunk = chunk;
                    break;
                }
            }

            if (targetChunk == null)
            {
                // Create a new chunk for this archetype
                targetChunk = new Chunk(archetype);
                chunks.Add(targetChunk);

                // Register this chunk with its component types
                foreach (var componentType in archetype.GetTypes())
                {
                    componentToChunks[componentType].Add(targetChunk);
                }
            }

            // Add the entity to the chunk
            if (targetChunk.TryAddEntity(entityId, components))
            {
                // Track the entity location
                entityLocations[entityId] = (targetChunk, entityId);
            }
            else
            {
                throw new InvalidOperationException($"Failed to add entity {entityId} to chunk");
            }
        }

        /// <summary>
        /// Removes an entity from the system
        /// </summary>
        public void RemoveEntity(int entityId)
        {
            if (!entityLocations.TryGetValue(entityId, out var location))
            {
                return; // Entity not found
            }

            var chunk = location.chunk;

            // Remove the entity from the chunk
            if (chunk.RemoveEntity(entityId))
            {
                // Remove the entity location
                entityLocations.Remove(entityId);

                // If the chunk is now empty, consider removing it
                if (chunk.IsEmpty)
                {
                    // For simplicity, we'll leave empty chunks in place for now
                    // In a more complete implementation, we'd recycle or remove them
                }
            }
        }

        /// <summary>
        /// Gets a direct reference to a component for an entity
        /// </summary>
        public ref T GetComponentRef<T>(int entityId)
            where T : struct
        {
            if (!entityLocations.TryGetValue(entityId, out var location))
            {
                throw new KeyNotFoundException($"Entity {entityId} not found");
            }

            var chunk = location.chunk;
            return ref chunk.GetComponentRef<T>(entityId);
        }

        /// <summary>
        /// Checks if an entity exists in the system
        /// </summary>
        public bool EntityExists(int entityId)
        {
            return entityLocations.ContainsKey(entityId);
        }

        /// <summary>
        /// Gets all chunks that contain a specific component type
        /// </summary>
        public List<Chunk> GetChunksWithComponent<T>()
            where T : struct
        {
            var componentType = ComponentType.Registry.GetComponentType<T>();
            if (!componentToChunks.TryGetValue(componentType, out var chunks))
            {
                return new List<Chunk>();
            }

            return new List<Chunk>(chunks);
        }

        /// <summary>
        /// Gets all chunks that contain all specified component types
        /// </summary>
        public List<Chunk> GetChunksWithComponents<T1, T2>()
            where T1 : struct
            where T2 : struct
        {
            var componentType1 = ComponentType.Registry.GetComponentType<T1>();
            var componentType2 = ComponentType.Registry.GetComponentType<T2>();

            if (
                !componentToChunks.TryGetValue(componentType1, out var chunks1)
                || !componentToChunks.TryGetValue(componentType2, out var chunks2)
            )
            {
                return new List<Chunk>();
            }

            // Find chunks containing both component types (intersection)
            var result = new List<Chunk>();
            foreach (var chunk in chunks1)
            {
                if (chunks2.Contains(chunk))
                {
                    result.Add(chunk);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets all chunks that contain all specified component types
        /// </summary>
        public List<Chunk> GetChunksWithComponents<T1, T2, T3>()
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var componentType1 = ComponentType.Registry.GetComponentType<T1>();
            var componentType2 = ComponentType.Registry.GetComponentType<T2>();
            var componentType3 = ComponentType.Registry.GetComponentType<T3>();

            if (
                !componentToChunks.TryGetValue(componentType1, out var chunks1)
                || !componentToChunks.TryGetValue(componentType2, out var chunks2)
                || !componentToChunks.TryGetValue(componentType3, out var chunks3)
            )
            {
                return new List<Chunk>();
            }

            // Find chunks containing all three component types
            var result = new List<Chunk>();
            foreach (var chunk in chunks1)
            {
                if (chunks2.Contains(chunk) && chunks3.Contains(chunk))
                {
                    result.Add(chunk);
                }
            }

            return result;
        }

        /// <summary>
        /// Process all entities with a specific component type
        /// </summary>
        public void ProcessComponents<T>(ActionRefWithEntity<T> action)
            where T : struct
        {
            List<Chunk> chunks = GetChunksWithComponent<T>();

            foreach (Chunk chunk in chunks)
            {
                T[] components = chunk.GetComponentArray<T>();
                int[] entityIds = chunk.GetEntityIds();

                for (int i = 0; i < entityIds.Length; i++)
                {
                    int entityId = entityIds[i];
                    action(ref components[i], entityId);
                }
            }
        }

        /// <summary>
        /// Process all entities with two specific component types
        /// </summary>
        public void ProcessComponentPairs<T1, T2>(ActionRefWithEntity<T1, T2> action)
            where T1 : struct
            where T2 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2>();

            foreach (var chunk in chunks)
            {
                var components1 = chunk.GetComponentArray<T1>();
                var components2 = chunk.GetComponentArray<T2>();
                var entityIds = chunk.GetEntityIds();

                for (int i = 0; i < entityIds.Length; i++)
                {
                    int entityId = entityIds[i];
                    action(ref components1[i], ref components2[i], entityId);
                }
            }
        }

        /// <summary>
        /// Process all entities with three specific component types
        /// </summary>
        public void ProcessComponentTriples<T1, T2, T3>(ActionRefWithEntity<T1, T2, T3> action)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2, T3>();

            foreach (var chunk in chunks)
            {
                var components1 = chunk.GetComponentArray<T1>();
                var components2 = chunk.GetComponentArray<T2>();
                var components3 = chunk.GetComponentArray<T3>();
                var entityIds = chunk.GetEntityIds();

                for (int i = 0; i < entityIds.Length; i++)
                {
                    int entityId = entityIds[i];
                    action(ref components1[i], ref components2[i], ref components3[i], entityId);
                }
            }
        }

        /// <summary>
        /// Process all entities with a specific component type in cache-friendly blocks
        /// </summary>
        public void ProcessComponentsInBlocks<T>(ActionRefWithEntity<T> action, int blockSize = 64)
            where T : struct
        {
            var chunks = GetChunksWithComponent<T>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessInBlocks(action, blockSize);
            }
        }

        /// <summary>
        /// Process all entities with two specific component types in cache-friendly blocks
        /// </summary>
        public void ProcessComponentPairsInBlocks<T1, T2>(
            ActionRefWithEntity<T1, T2> action,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessInBlocks(action, blockSize);
            }
        }

        /// <summary>
        /// Process all entities with three specific component types in cache-friendly blocks
        /// </summary>
        public void ProcessComponentTriplesInBlocks<T1, T2, T3>(
            ActionRefWithEntity<T1, T2, T3> action,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2, T3>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessInBlocks(action, blockSize);
            }
        }

        /// <summary>
        /// Process all entities with block access to component arrays
        /// </summary>
        public void ProcessComponentsInBlock<T>(BlockAction<T> blockAction, int blockSize = 64)
            where T : struct
        {
            var chunks = GetChunksWithComponent<T>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessBlock(blockAction, blockSize);
            }
        }

        /// <summary>
        /// Process all entities with block access to component arrays
        /// </summary>
        public void ProcessComponentPairsInBlock<T1, T2>(
            BlockAction<T1, T2> blockAction,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessBlock(blockAction, blockSize);
            }
        }

        /// <summary>
        /// Process all entities with block access to component arrays
        /// </summary>
        public void ProcessComponentTriplesInBlock<T1, T2, T3>(
            BlockAction<T1, T2, T3> blockAction,
            int blockSize = 64
        )
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var chunks = GetChunksWithComponents<T1, T2, T3>();

            foreach (var chunk in chunks)
            {
                chunk.ProcessBlock(blockAction, blockSize);
            }
        }
    }
}
