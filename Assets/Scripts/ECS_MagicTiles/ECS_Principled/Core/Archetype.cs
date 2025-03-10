using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// Represents a unique combination of component types
    /// Enhanced with dynamic creation and equality comparison
    /// </summary>
    public class Archetype : IEquatable<Archetype>
    {
        // Component types in this archetype, sorted by ID for deterministic access
        private readonly ComponentType[] types;
        
        // Pre-calculate hash code for fast dictionary operations
        private readonly int hash;
        
        // Flag for aspect archetypes (for queries)
        private readonly bool isAspect;
        
        // Cache for component index lookups
        private readonly Dictionary<ComponentType, int> typeToIndex;
        
        // Signature for debugging
        private readonly string signature;

        public bool IsAspect => isAspect;

        /// <summary>
        /// Create a new archetype from an array of component types
        /// </summary>
        public Archetype(ComponentType[] componentTypes, bool isAspect = false)
        {
            // Store types in a fixed order for consistent hashing
            types = componentTypes.OrderBy(t => t.Id).ToArray();
            hash = CalculateHash(types);
            this.isAspect = isAspect;
            
            // Create lookup for component indices
            typeToIndex = new Dictionary<ComponentType, int>();
            for (int i = 0; i < types.Length; i++)
            {
                typeToIndex[types[i]] = i;
            }
            
            // Create signature for debugging
            signature = string.Join(",", types.Select(t => t.Type.Name));
        }

        /// <summary>
        /// Creates an archetype from an array of object instances
        /// </summary>
        public static Archetype FromComponents(object[] components)
        {
            // Extract component types
            ComponentType[] componentTypes = new ComponentType[components.Length];
            for (int i = 0; i < components.Length; i++)
            {
                Type componentType = components[i].GetType();
                componentTypes[i] = ComponentType.Registry.GetComponentType(componentType);
            }
            
            return new Archetype(componentTypes);
        }

        /// <summary>
        /// Get the component types in this archetype
        /// </summary>
        public ComponentType[] GetTypes() => types;

        /// <summary>
        /// Get the hash code for this archetype
        /// </summary>
        public int GetHash() => hash;
        
        /// <summary>
        /// Get the index of a specific component type in this archetype
        /// </summary>
        public int GetComponentIndex(ComponentType componentType)
        {
            if (typeToIndex.TryGetValue(componentType, out int index))
            {
                return index;
            }
            return -1; // Component not found in this archetype
        }
        
        /// <summary>
        /// Checks if this archetype contains a specific component type
        /// </summary>
        public bool HasComponent(ComponentType componentType)
        {
            return typeToIndex.ContainsKey(componentType);
        }
        
        /// <summary>
        /// Get the index of a specific component type in this archetype
        /// </summary>
        public int GetComponentIndex<T>() where T : struct
        {
            var componentType = ComponentType.Registry.GetComponentType<T>();
            return GetComponentIndex(componentType);
        }
        
        /// <summary>
        /// Checks if this archetype contains a specific component type
        /// </summary>
        public bool HasComponent<T>() where T : struct
        {
            var componentType = ComponentType.Registry.GetComponentType<T>();
            return HasComponent(componentType);
        }

        /// <summary>
        /// Calculate a hash code for this archetype based on component types
        /// </summary>
        private static int CalculateHash(ComponentType[] types)
        {
            // Simple but effective hash calculation for fixed component combinations
            int hash = 17;
            foreach (var type in types)
            {
                hash = hash * 31 + type.Id;
            }
            return hash;
        }
        
        /// <summary>
        /// Check if this archetype equals another
        /// </summary>
        public bool Equals(Archetype other)
        {
            if (other == null || types.Length != other.types.Length)
                return false;
                
            // First check hash for quick non-equality
            if (hash != other.hash)
                return false;
                
            // Then check all component types to be sure
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].Id != other.types[i].Id)
                    return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Check if this archetype equals another object
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Archetype);
        }
        
        /// <summary>
        /// Get the hash code for this archetype
        /// </summary>
        public override int GetHashCode()
        {
            return hash;
        }
        
        /// <summary>
        /// Get a string representation of this archetype
        /// </summary>
        public override string ToString()
        {
            return $"Archetype({signature})";
        }

        /// <summary>
        /// Registry of common archetypes in the game
        /// </summary>
        public static class Registry
        {
            // Note archetypes
            public static readonly Archetype MusicNote = new Archetype(
                new[]
                {
                    ComponentType.Registry.Transform,
                    ComponentType.Registry.MusicNote,
                    ComponentType.Registry.Corner,
                    ComponentType.Registry.MusicNoteInteraction,
                    ComponentType.Registry.MusicNoteFiller,
                    ComponentType.Registry.NoteScoreState,
                }
            );

            // Game setup archetypes
            public static readonly Archetype PerfectLine = new Archetype(
                new[]
                {
                    ComponentType.Registry.Transform,
                    ComponentType.Registry.PerfectLine,
                    ComponentType.Registry.Corner,
                }
            );

            public static readonly Archetype Input = new Archetype(
                new[] { ComponentType.Registry.Input }
            );
            
            public static readonly Archetype StartingNote = new Archetype(
                new[]
                {
                    ComponentType.Registry.Transform,
                    ComponentType.Registry.ActiveState,
                    ComponentType.Registry.StartingNote,
                }
            );

            public static readonly Archetype GameScore = new Archetype(
                new[] { ComponentType.Registry.GameScore }
            );

            public static readonly Archetype SongProgress = new Archetype(
                new[] { ComponentType.Registry.Progress }
            );

            public static readonly Archetype LaneLines = new Archetype(
                new[] { ComponentType.Registry.Transform }
            );

            public static readonly Archetype WorldState = new Archetype(
                new[] { ComponentType.Registry.WorldState }
            );

            // Keep track of all archetypes for initialization
            private static readonly Archetype[] AllArchetypes =
            {
                MusicNote,
                PerfectLine,
                Input,
                StartingNote,
                GameScore,
                SongProgress,
                LaneLines,
                WorldState,
            };

            public static Archetype[] GetAllArchetypes() => AllArchetypes;
            
            // Archetype cache for faster lookup
            private static readonly Dictionary<string, Archetype> archetypeCache = 
                new Dictionary<string, Archetype>();
                
            /// <summary>
            /// Get or create an archetype from component types
            /// </summary>
            public static Archetype GetOrCreate(ComponentType[] componentTypes)
            {
                // Sort by component ID for consistent archetype identity
                Array.Sort(componentTypes, (a, b) => a.Id.CompareTo(b.Id));
                
                // Create a unique key for this set of component types
                string archetypeKey = string.Join(",", componentTypes.Select(t => t.Id));
                
                // Check if we already have this archetype
                if (archetypeCache.TryGetValue(archetypeKey, out var existingArchetype))
                {
                    return existingArchetype;
                }
                
                // Create a new archetype and cache it
                var newArchetype = new Archetype(componentTypes);
                archetypeCache[archetypeKey] = newArchetype;
                
                return newArchetype;
            }
        }
    }
}