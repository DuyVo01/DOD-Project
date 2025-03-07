using ECS_MagicTile.Components;
using EventChannel;

namespace ECS_MagicTile
{
    public class StartingNoteSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        private readonly IntEventChannel OnGameStartChannel;

        ArchetypeStorage startingNoteStorage;
        ActiveStateComponent[] startingNoteActiveState;
        private StartingNoteSyncTool startingNoteSyncTool;

        private GeneralGameSetting generalGameSetting;

        private int eventListenerId;

        public StartingNoteSystem(GlobalPoint globalPoint)
        {
            OnGameStartChannel = globalPoint.OnGameStartChannel;

            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
            generalGameSetting = globalPoint.generalGameSetting;
        }

        public void RunCleanup()
        {
            OnGameStartChannel.Unsubscribe(eventListenerId);
        }

        public void RunInitialize()
        {
            startingNoteStorage = World.GetStorage(Archetype.Registry.StartingNote);

            startingNoteActiveState = startingNoteStorage.GetComponents<ActiveStateComponent>();

            eventListenerId = OnGameStartChannel.Subscribe(
                target: this,
                (target, data) => OnStartNoteInteraction(data)
            );
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime)
        {
            //
        }

        private void OnStartNoteInteraction(int startNoteId)
        {
            SystemRegistry.SetGameState(EGameState.IngamePlaying);
            startingNoteActiveState[0].isActive = false;
            startingNoteSyncTool.SyncStartNoteState(startingNoteActiveState[0]);
            generalGameSetting.CurrentGameState = EGameState.IngamePlaying;
        }
    }
}
