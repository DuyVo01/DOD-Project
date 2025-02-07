using UnityEngine;

namespace ECS_MagicTile
{
    public class ProgressSyncer : ArchetypeSyncer
    {
        public override EGameState GameStateToExecute => EGameState.Ingame;

        protected override Archetype Archetype => Archetype.Registry.SongProgress;
    }
}
