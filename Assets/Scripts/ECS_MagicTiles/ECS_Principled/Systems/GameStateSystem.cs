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

        private readonly IntEventChannel startNoteEntityIdChannel;

        ArchetypeStorage startingNoteStorage;
        ActiveStateComponent[] startingNoteActiveState;
        private StartingNoteSyncTool startingNoteSyncTool;

        public GameStateSystem(GlobalPoint globalPoint)
        {
            startNoteEntityIdChannel = globalPoint.entityIdChannel;

            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
        }

        public void Cleanup()
        {
            //
        }

        public void Initialize()
        {
            startingNoteStorage = World.GetStorage(Archetype.Registry.StartingNote);

            startingNoteActiveState = startingNoteStorage.GetComponents<ActiveStateComponent>();

            startNoteEntityIdChannel.Subscribe(OnStartNoteInteraction);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            //
        }

        private void OnStartNoteInteraction(int startNoteId)
        {
            SystemRegistry.SetGameState(EGameState.Ingame);
            startingNoteActiveState[0].isActive = false;
            startingNoteSyncTool.SyncStartNoteState(startingNoteActiveState[0]);
        }
    }
}
