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

        public GameStateSystem(GlobalPoint globalPoint)
        {
            startNoteEntityIdChannel = globalPoint.entityIdChannel;
        }

        public void Cleanup()
        {
            //
        }

        public void Initialize()
        {
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
            ArchetypeStorage storage = World.GetStorage(Archetype.Registry.StartingNote);
            storage.GetComponents<ActiveStateComponent>()[0].isActive = false;
            SystemRegistry.SetGameState(EGameState.Ingame);
        }
    }
}
