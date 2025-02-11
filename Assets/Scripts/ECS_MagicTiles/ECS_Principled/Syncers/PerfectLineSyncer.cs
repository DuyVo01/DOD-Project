using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class PerfectLineSyncer : ArchetypeSyncer
    {
        public override EGameState GameStateToExecute => EGameState.All;

        protected override Archetype Archetype => Archetype.Registry.PerfectLine;

        private GameObject perfectLineGO;

        ArchetypeStorage perfectLineStorage;
        TransformComponent[] perfectLineTransform;

        public PerfectLineSyncer(GlobalPoint globalPoint)
        {
            perfectLineGO = globalPoint.perfectLineObject;
        }

        public override void Initialize()
        {
            perfectLineStorage = World.GetStorage(Archetype);
            perfectLineTransform = perfectLineStorage.GetComponents<TransformComponent>();
        }

        public override void Update(float deltaTime)
        {
            perfectLineGO.transform.position = perfectLineTransform[0].Posision;
        }
    }
}
