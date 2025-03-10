using System;
using System.Collections.Generic;
using ECS_MagicTile.Components;

namespace ECS_MagicTile
{
    /// <summary>
    /// Represents a component type in the ECS system with enhanced dynamic registration
    /// </summary>
    public readonly struct ComponentType : IEquatable<ComponentType>
    {
        public readonly Type Type;
        public readonly int Id;

        // Private constructor to ensure all types are created through registry
        private ComponentType(Type type, int id)
        {
            Type = type;
            Id = id;
        }

        public bool Equals(ComponentType other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is ComponentType other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"ComponentType({Type.Name}, {Id})";
        }

        /// <summary>
        /// Registry for managing component types, enhanced with dynamic registration
        /// </summary>
        public static class Registry
        {
            // Predefined components
            public static readonly ComponentType Transform = new ComponentType(
                typeof(TransformComponent),
                0
            );
            public static readonly ComponentType MusicNote = new ComponentType(
                typeof(MusicNoteComponent),
                1
            );
            public static readonly ComponentType PerfectLine = new ComponentType(
                typeof(PerfectLineTagComponent),
                3
            );
            public static readonly ComponentType Corner = new ComponentType(
                typeof(CornerComponent),
                4
            );
            public static readonly ComponentType Input = new ComponentType(
                typeof(InputStateComponent),
                5
            );
            public static readonly ComponentType MusicNoteInteraction = new ComponentType(
                typeof(MusicNoteInteractionComponent),
                6
            );
            public static readonly ComponentType MusicNoteFiller = new ComponentType(
                typeof(MusicNoteFillerComponent),
                7
            );
            public static readonly ComponentType ActiveState = new ComponentType(
                typeof(ActiveStateComponent),
                8
            );
            public static readonly ComponentType StartingNote = new ComponentType(
                typeof(StartingNoteTagComponent),
                9
            );
            public static readonly ComponentType GameScore = new ComponentType(
                typeof(ScoreComponent),
                10
            );
            public static readonly ComponentType NoteScoreState = new ComponentType(
                typeof(ScoreStateComponent),
                11
            );
            public static readonly ComponentType TransformGroup = new ComponentType(
                typeof(TransformComponentGroup),
                12
            );
            public static readonly ComponentType Progress = new ComponentType(
                typeof(ProgressComponent),
                14
            );
            public static readonly ComponentType WorldState = new ComponentType(
                typeof(WorldStateComponent),
                15
            );

            // Next available ID for dynamically registered components
            private static int nextId = 16;

            // Lookup dictionaries
            private static readonly Dictionary<Type, ComponentType> typeToComponentType =
                new Dictionary<Type, ComponentType>
                {
                    { typeof(TransformComponent), Transform },
                    { typeof(MusicNoteComponent), MusicNote },
                    { typeof(PerfectLineTagComponent), PerfectLine },
                    { typeof(CornerComponent), Corner },
                    { typeof(InputStateComponent), Input },
                    { typeof(MusicNoteInteractionComponent), MusicNoteInteraction },
                    { typeof(MusicNoteFillerComponent), MusicNoteFiller },
                    { typeof(ActiveStateComponent), ActiveState },
                    { typeof(StartingNoteTagComponent), StartingNote },
                    { typeof(ScoreComponent), GameScore },
                    { typeof(ScoreStateComponent), NoteScoreState },
                    { typeof(TransformComponentGroup), TransformGroup },
                    { typeof(ProgressComponent), Progress },
                    { typeof(WorldStateComponent), WorldState },
                };

            /// <summary>
            /// Gets a component type for a specific type, dynamically registering if not found
            /// </summary>
            public static ComponentType GetComponentType(Type type)
            {
                if (typeToComponentType.TryGetValue(type, out var componentType))
                {
                    return componentType;
                }

                // Dynamically register the new component type
                componentType = new ComponentType(type, nextId++);
                typeToComponentType[type] = componentType;
                
                // Log when we dynamically register a new component type
                UnityEngine.Debug.Log($"Dynamically registered new component type: {type.Name} with ID {componentType.Id}");
                
                return componentType;
            }

            /// <summary>
            /// Gets a component type for a specific generic type
            /// </summary>
            public static ComponentType GetComponentType<T>() where T : struct
            {
                return GetComponentType(typeof(T));
            }

            // The existing methods for compatibility
            public static ComponentType[] GetAllTypes() => AllTypes;

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