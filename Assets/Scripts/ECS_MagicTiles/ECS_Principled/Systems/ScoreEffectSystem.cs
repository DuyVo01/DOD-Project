using EventChannel;
using PrimeTween;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ScoreEffectSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.Ingame;

        private readonly BoolEventChannel scoreEffectChannel;

        public ScoreEffectSystem(GlobalPoint globalPoint)
        {
            this.scoreEffectChannel = globalPoint.scoreEffectChannel;
        }

        public void Cleanup()
        {
            scoreEffectChannel.Unsubscribe(ExecuteEffect);
        }

        public void Initialize()
        {
            scoreEffectChannel.Subscribe(ExecuteEffect);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            //
        }

        private void ExecuteEffect(bool isPerfect)
        {
            ArchetypeStorage storage = World.GetStorage(Archetype.Registry.ScoreEffect);
        }
    }
}
