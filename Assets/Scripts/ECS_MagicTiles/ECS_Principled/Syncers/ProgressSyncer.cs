using EventChannel;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class ProgressSyncer : ArchetypeSyncer
    {
        public override EGameState GameStateToExecute => EGameState.IngamePlaying;

        protected override Archetype Archetype => Archetype.Registry.SongProgress;

        private readonly BoolEventChannel scoreEventChannel;

        private readonly Slider progressSlider;

        private ProgressComponent[] progressComponents;

        private int eventListenerId;

        public ProgressSyncer(GlobalPoint globalPoint)
        {
            scoreEventChannel = globalPoint.OnScoreHitChannel;
            progressSlider = globalPoint.progressSlider;
        }

        public override void RunInitialize()
        {
            IsEnabled = true;

            progressComponents = DedicatedStorage.GetComponents<ProgressComponent>();

            eventListenerId = scoreEventChannel.Subscribe(
                target: this,
                (target, data) => SyncProgressToView(data)
            );
        }

        private void SyncProgressToView(bool isPerfect)
        {
            ProgressComponent progressComponent = progressComponents[0];
            progressSlider.value = progressComponent.currentProgressPercent;
        }
    }
}
