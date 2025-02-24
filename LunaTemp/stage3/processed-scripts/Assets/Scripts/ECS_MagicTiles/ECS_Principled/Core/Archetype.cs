using System;
using System.Linq;

namespace ECS_MagicTile
{
    public class Archetype
    {
        private readonly ComponentType[] types;
        private readonly int hash;
        private readonly bool isAspect;

        public bool IsAspect => isAspect;

        private Archetype(ComponentType[] types, bool isAspect = false)
        {
            // Store types in a fixed order for consistent hashing
            this.types = types.OrderBy(t => t.Id).ToArray();
            this.hash = CalculateHash(this.types);
            this.isAspect = isAspect;
        }

        // Utility methods for archetype management
        public ReadOnlySpan<ComponentType> GetTypes() => types;

        public int GetHash() => hash;

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

        // Game-specific archetypes
        public static class Registry
        {
            // Note archetypes
            public static readonly Archetype MusicNote =
                new ECS_MagicTile.Archetype(
                    new[]
                    {
                        ComponentType.Registry.Transform,
                        ComponentType.Registry.MusicNote,
                        ComponentType.Registry.Corner, // Added CornerComponent
                        ComponentType.Registry.MusicNoteInteraction,
                        ComponentType.Registry.MusicNoteFiller,
                        ComponentType.Registry.NoteScoreState,
                    }
                );

            // Game setup archetypes
            public static readonly Archetype PerfectLine =
                new ECS_MagicTile.Archetype(
                    new[]
                    {
                        ComponentType.Registry.Transform,
                        ComponentType.Registry.PerfectLine,
                        ComponentType.Registry.Corner,
                    }
                );

            public static readonly Archetype Input = new ECS_MagicTile.Archetype(new[] { ComponentType.Registry.Input });
            public static readonly Archetype StartingNote =
                new ECS_MagicTile.Archetype(
                    new[]
                    {
                        ComponentType.Registry.Transform,
                        ComponentType.Registry.ActiveState,
                        ComponentType.Registry.StartingNote,
                    }
                );

            public static readonly Archetype GameScore =
                new ECS_MagicTile.Archetype(
                    new[]
                    {
                        ComponentType
                            .Registry
                            .GameScore // New score component
                        ,
                    }
                );

            public static readonly Archetype SongProgress =
                new ECS_MagicTile.Archetype(new[] { ComponentType.Registry.Progress });

            public static readonly Archetype LaneLines =
                new ECS_MagicTile.Archetype(new[] { ComponentType.Registry.Transform });

            public static readonly Archetype WorldState =
                new ECS_MagicTile.Archetype(new[] { ComponentType.Registry.WorldState });

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

            public static ReadOnlySpan<Archetype> GetAllArchetypes() => AllArchetypes;
        }
    }
}
