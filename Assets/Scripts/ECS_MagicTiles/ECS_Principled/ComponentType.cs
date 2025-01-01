using System;
using System.Collections.Generic;

namespace ECS_MagicTile
{
    public readonly struct ComponentType
    {
        public readonly Type Type;
        public readonly int Id;

        // Private constructor ensures all types are created through registry
        private ComponentType(Type type, int id)
        {
            Type = type;
            Id = id;
        }

        // Static registry of all component types in our game
        public static class Registry
        {
            // Core Components
            public static readonly ComponentType Transform = new(typeof(TransformComponent), 0);
            public static readonly ComponentType MusicNote = new(typeof(MusicNoteComponent), 1);

            public static readonly ComponentType PerfectLine =
                new(typeof(PerfectLineTagComponent), 3);
            public static readonly ComponentType Corner = new(typeof(CornerComponent), 4);

            // A lookup dictionary to quickly find ComponentType by Type
            private static readonly Dictionary<Type, ComponentType> typeToComponentType;

            // Initialize our lookup dictionary when the class is first used
            static Registry()
            {
                typeToComponentType = new Dictionary<Type, ComponentType>
                {
                    { typeof(TransformComponent), Transform },
                    { typeof(MusicNoteComponent), MusicNote },
                    { typeof(PerfectLineTagComponent), PerfectLine },
                    { typeof(CornerComponent), Corner },
                };
            }

            // This is our new generic lookup method
            public static ComponentType GetComponentType<T>()
                where T : struct
            {
                var type = typeof(T);
                if (!typeToComponentType.TryGetValue(type, out var componentType))
                {
                    throw new InvalidOperationException(
                        $"Component type {type.Name} is not registered. "
                            + "Make sure to add it to the ComponentType.Registry."
                    );
                }
                return componentType;
            }

            // The existing methods remain the same
            public static ReadOnlySpan<ComponentType> GetAllTypes() => AllTypes;

            private static readonly ComponentType[] AllTypes =
            {
                Transform,
                MusicNote,
                PerfectLine,
                Corner,
            };
        }
    }
}
