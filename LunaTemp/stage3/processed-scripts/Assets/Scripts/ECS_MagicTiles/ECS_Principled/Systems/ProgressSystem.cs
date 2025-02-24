using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ProgressSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.Ingame;

        private BoolEventChannel scoreEventChannel;

        private ProgressSyncTool progressSyncTool;

        public ProgressSystem(GlobalPoint globalPoint)
        {
            this.scoreEventChannel = globalPoint.OnScoreHitChannel;
            progressSyncTool = globalPoint.progressSyncTool;
        }

        private ArchetypeStorage progressArchetype;

        public void Cleanup() { }

        public void Initialize()
        {
            progressArchetype = World.GetStorage(Archetype.Registry.SongProgress);

            scoreEventChannel.Subscribe(OnScoreEvent);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime) { }

        private void OnScoreEvent(bool isScore)
        {
            ref ProgressComponent progress =
                ref progressArchetype.GetComponents<ProgressComponent>()[0];
            progress.CurrentProgressRawValue++;
            progress.currentProgressPercent =
                progress.CurrentProgressRawValue / progress.MaxProgressRawValue;

            Debug.Log($"Current Progress: {progress.currentProgressPercent}");

            progressSyncTool.SycnProgress(progress);
        }
    }
}
