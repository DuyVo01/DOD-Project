using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// Specialized storage for a single entity's components
    /// Acts like a micro-chunk that holds only one entity's data
    /// </summary>
    public class SingletonChunk
    {
        // The entity ID this chunk is associated with
        public int EntityId { get; }

        // Archetype information (for consistency with the ECS model)
        public Archetype Archetype { get; }

        // Each component type is stored as a single-element array
        // This allows us to return proper references while maintaining type safety
        private Dictionary<Type, Array> components;

        // Flag type that marks this singleton
        public Type FlagType { get; }

        public SingletonChunk(int entityId, Archetype archetype, Type flagType)
        {
            EntityId = entityId;
            Archetype = archetype;
            FlagType = flagType;
            components = new Dictionary<Type, Array>();
        }

        /// <summary>
        /// Sets a component value
        /// </summary>
        public void SetComponent<T>(T component)
            where T : struct, IComponent
        {
            if (!components.TryGetValue(typeof(T), out var componentArray))
            {
                // Create a new single-element array for this component type
                T[] newArray = new T[1];
                components[typeof(T)] = newArray;
                componentArray = newArray;
            }

            // Set the component value in the array
            T[] typedArray = (T[])componentArray;
            typedArray[0] = component;
        }

        /// <summary>
        /// Sets a component value by type
        /// </summary>
        public void SetComponent(Type componentType, object component)
        {
            if (!components.TryGetValue(componentType, out var componentArray))
            {
                // Create a new single-element array for this component type
                Array newArray = Array.CreateInstance(componentType, 1);
                components[componentType] = newArray;
                componentArray = newArray;
            }

            // Set the component value in the array
            componentArray.SetValue(component, 0);
        }

        /// <summary>
        /// Gets a reference to a component
        /// </summary>
        public ref T GetComponent<T>()
            where T : struct, IComponent
        {
            if (!components.TryGetValue(typeof(T), out var componentArray))
            {
                // Create a new single-element array for this component type
                T[] newArray = new T[1] { default };
                components[typeof(T)] = newArray;
                componentArray = newArray;
            }

            // Cast to the correct array type and return a reference to the first element
            Span<T> typedArray = new Span<T>((T[])componentArray);

            return ref typedArray[0];
        }

        /// <summary>
        /// Checks if the chunk has a specific component
        /// </summary>
        public bool HasComponent<T>()
            where T : struct, IComponent
        {
            return components.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Checks if the chunk has a specific component
        /// </summary>
        public bool HasComponent(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        /// <summary>
        /// Gets all component types in this chunk
        /// </summary>
        public IEnumerable<Type> GetComponentTypes()
        {
            return components.Keys;
        }
    }
}
