using ECS_MagicTile.Components;
using EventChannel;

namespace ECS_MagicTile
{
    public class StartingNoteSystem : GameSystemBase
    {
        private readonly IntEventChannel OnGameStartChannel;
        private readonly StartingNoteSyncTool startingNoteSyncTool;
        private readonly GeneralGameSetting generalGameSetting;
        private int eventListenerId;

        public StartingNoteSystem(GlobalPoint globalPoint)
        {
            OnGameStartChannel = globalPoint.OnGameStartChannel;
            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
            generalGameSetting = globalPoint.generalGameSetting;
        }

        protected override void Initialize()
        {
            // Subscribe to the game start event
            eventListenerId = OnGameStartChannel.Subscribe(
                target: this,
                (target, data) => OnStartNoteInteraction(data)
            );
        }

        protected override void Execute(float deltaTime)
        {
            // No per-frame logic needed
        }

        protected override void Cleanup()
        {
            // Unsubscribe from event when system is cleaned up
            OnGameStartChannel.Unsubscribe(eventListenerId);
        }

        private void OnStartNoteInteraction(int startNoteId)
        {
            // Get a direct reference to the active state component of the starting note singleton
            ref var activeState = ref World.GetSingleton<StartingNoteTagComponent, ActiveStateComponent>();
            
            // Update game state
            SystemRegistry.SetGameState(EGameState.IngamePlaying);
            
            // Deactivate the starting note
            activeState.IsActive = false;
            
            // Sync the state change to the visual representation
            startingNoteSyncTool.SyncSingleton();
            
            // Update global game state
            generalGameSetting.CurrentGameState = EGameState.IngamePlaying;
        }
    }
}
