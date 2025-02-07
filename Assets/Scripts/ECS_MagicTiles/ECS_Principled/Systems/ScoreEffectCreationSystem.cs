using ECS_MagicTile.Components;
using ECS_MagicTile.Settings;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ScoreEffectCreationSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.WaitingToStart;

        public ScoreEffectCreationSystem(GlobalPoint globalPoint) { }

        public void Cleanup()
        {
            //
        }

        public void Initialize() { }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            //
        }
    }
}
