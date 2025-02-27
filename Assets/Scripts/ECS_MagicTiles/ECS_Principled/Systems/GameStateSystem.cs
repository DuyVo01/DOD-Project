using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class GameStateSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.All;

        private readonly IntEventChannel OnGameStartChannel;

        ArchetypeStorage startingNoteStorage;
        ActiveStateComponent[] startingNoteActiveState;
        private StartingNoteSyncTool startingNoteSyncTool;

        private GeneralGameSetting generalGameSetting;

        public GameStateSystem(GlobalPoint globalPoint)
        {
            OnGameStartChannel = globalPoint.OnGameStartChannel;

            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
            generalGameSetting = globalPoint.generalGameSetting;
        }

        public void RunCleanup()
        {
            //
        }

        public void RunInitialize()
        {
            startingNoteStorage = World.GetStorage(Archetype.Registry.StartingNote);

            startingNoteActiveState = startingNoteStorage.GetComponents<ActiveStateComponent>();

            OnGameStartChannel.Subscribe(OnStartNoteInteraction);
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
