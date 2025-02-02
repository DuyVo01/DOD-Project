using System;
using System.Collections.Generic;
using ECS_MagicTile.Components;
using NUnit.Framework.Constraints;

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

            public static readonly ComponentType Input = new(typeof(InputStateComponent), 5);
            public static readonly ComponentType MusicNoteInteraction =
                new(typeof(MusicNoteInteractionComponent), 6);
            public static readonly ComponentType MusicNoteFiller =
                new(typeof(MusicNoteFillerComponent), 7);

            public static readonly ComponentType ActiveState = new(typeof(ActiveStateComponent), 8);
            public static readonly ComponentType StartingNote =
                new(typeof(StartingNoteTagComponent), 9);
            public static readonly ComponentType GameScore = new(typeof(ScoreComponent), 10);
            public static readonly ComponentType NoteScoreState =
                new(typeof(ScoreStateComponent), 11);

            // A lookup dictionary to quickly find ComponentType by Type
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
                };

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
