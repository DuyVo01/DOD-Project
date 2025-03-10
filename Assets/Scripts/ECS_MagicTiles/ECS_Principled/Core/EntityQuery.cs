using System;
using System.Collections.Generic;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    /// <summary>
    /// Provides a fluent interface for querying and processing entities
    /// </summary>
    public class EntityQuery
    {
        private readonly ChunkManager chunkManager;
        
        // Expose the chunk manager for extensions
        internal ChunkManager ChunkManager => chunkManager;
        
        /// <summary>
        /// Creates a new entity query
        /// </summary>
        internal EntityQuery(ChunkManager chunkManager)
        {
            this.chunkManager = chunkManager;
        }
        
        /// <summary>
        /// Process all entities with a specific component type
        /// </summary>
        public EntityQuery ForEach<T>(ActionRef<T> action) where T : struct
        {
            chunkManager.ProcessComponents<T>((ref T component, int entityId) => {
                action(ref component);
            });
            
            return this;
        }
        
        /// <summary>
        /// Process all entities with a specific component type, providing entity ID
        /// </summary>
        public EntityQuery ForEach<T>(ActionRefWithEntity<T> action) where T : struct
        {
            chunkManager.ProcessComponents<T>(action);
            return this;
        }
        
        /// <summary>
        /// Process all entities with two specific component types
        /// </summary>
        public EntityQuery ForEach<T1, T2>(ActionRef<T1, T2> action)
            where T1 : struct
            where T2 : struct
        {
            chunkManager.ProcessComponentPairs<T1, T2>((ref T1 c1, ref T2 c2, int entityId) => {
                action(ref c1, ref c2);
            });
            
            return this;
        }
        
        /// <summary>
        /// Process all entities with two specific component types, providing entity ID
        /// </summary>
        public EntityQuery ForEach<T1, T2>(ActionRefWithEntity<T1, T2> action)
            where T1 : struct
            where T2 : struct
        {
            chunkManager.ProcessComponentPairs<T1, T2>(action);
            return this;
        }
        
        /// <summary>
        /// Process all entities with three specific component types
        /// </summary>
        public EntityQuery ForEach<T1, T2, T3>(ActionRef<T1, T2, T3> action)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            chunkManager.ProcessComponentTriples<T1, T2, T3>((ref T1 c1, ref T2 c2, ref T3 c3, int entityId) => {
                action(ref c1, ref c2, ref c3);
            });
            
            return this;
        }
        
        /// <summary>
        /// Process all entities with three specific component types, providing entity ID
        /// </summary>
        public EntityQuery ForEach<T1, T2, T3>(ActionRefWithEntity<T1, T2, T3> action)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            chunkManager.ProcessComponentTriples<T1, T2, T3>(action);
            return this;
        }
        
        /// <summary>
        /// Process all entities with a specific component type in cache-friendly blocks
        /// </summary>
        public EntityQuery ForEachBlock<T>(ActionRefWithEntity<T> action, int blockSize = 64)
            where T : struct
        {
            chunkManager.ProcessComponentsInBlocks<T>(action, blockSize);
            return this;
        }
        
        /// <summary>
        /// Process all entities with two specific component types in cache-friendly blocks
        /// </summary>
        public EntityQuery ForEachBlock<T1, T2>(ActionRefWithEntity<T1, T2> action, int blockSize = 64)
            where T1 : struct
            where T2 : struct
        {
            chunkManager.ProcessComponentPairsInBlocks<T1, T2>(action, blockSize);
            return this;
        }
        
        /// <summary>
        /// Process all entities with three specific component types in cache-friendly blocks
        /// </summary>
        public EntityQuery ForEachBlock<T1, T2, T3>(ActionRefWithEntity<T1, T2, T3> action, int blockSize = 64)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            chunkManager.ProcessComponentTriplesInBlocks<T1, T2, T3>(action, blockSize);
            return this;
        }
        
        /// <summary>
        /// Advanced block processing with direct array access
        /// </summary>
        public EntityQuery ForEachBlockDirect<T>(BlockAction<T> blockAction, int blockSize = 64)
            where T : struct
        {
            chunkManager.ProcessComponentsInBlock<T>(blockAction, blockSize);
            return this;
        }
        
        /// <summary>
        /// Advanced block processing with direct array access
        /// </summary>
        public EntityQuery ForEachBlockDirect<T1, T2>(BlockAction<T1, T2> blockAction, int blockSize = 64)
            where T1 : struct
            where T2 : struct
        {
            chunkManager.ProcessComponentPairsInBlock<T1, T2>(blockAction, blockSize);
            return this;
        }
        
        /// <summary>
        /// Advanced block processing with direct array access
        /// </summary>
        public EntityQuery ForEachBlockDirect<T1, T2, T3>(BlockAction<T1, T2, T3> blockAction, int blockSize = 64)
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            chunkManager.ProcessComponentTriplesInBlock<T1, T2, T3>(blockAction, blockSize);
            return this;
        }
    }
}