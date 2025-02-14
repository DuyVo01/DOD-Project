using EventChannel;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class ProgressSyncer : ArchetypeSyncer
    {
        public override EGameState GameStateToExecute => EGameState.Ingame;

        protected override Archetype Archetype => Archetype.Registry.SongProgress;

        private readonly BoolEventChannel scoreEventChannel;

        private readonly Slider progressSlider;

        private ProgressComponent[] progressComponents;

        public ProgressSyncer(GlobalPoint globalPoint)
        {
            scoreEventChannel = globalPoint.OnScoreHitChannel;
            progressSlider = globalPoint.progressSlider;
        }

        public override void Initialize()
        {
            IsEnabled = true;

            progressComponents = DedicatedStorage.GetComponents<ProgressComponent>();

            scoreEventChannel.Subscribe(SyncProgressToView);
        }

        private void SyncProgressToView(bool isPerfect)
        {
            ProgressComponent progressComponent = progressComponents[0];
            progressSlider.value = progressComponent.currentProgressPercent;
        }
    }
}
