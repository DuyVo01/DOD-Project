using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ProgressSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.IngamePlaying;

        private BoolEventChannel scoreEventChannel;

        private ProgressSyncTool progressSyncTool;

        public ProgressSystem(GlobalPoint globalPoint)
        {
            this.scoreEventChannel = globalPoint.OnScoreHitChannel;
            progressSyncTool = globalPoint.progressSyncTool;
        }

        private ArchetypeStorage progressArchetype;

        private int eventSubscriptionId;

        public void RunCleanup()
        {
            scoreEventChannel.Unsubscribe(eventSubscriptionId);
        }

        public void RunInitialize()
        {
            progressArchetype = World.GetStorage(Archetype.Registry.SongProgress);

            eventSubscriptionId = scoreEventChannel.Subscribe(
                target: this,
                (target, data) => OnScoreEvent(data)
            );
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime) { }

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
