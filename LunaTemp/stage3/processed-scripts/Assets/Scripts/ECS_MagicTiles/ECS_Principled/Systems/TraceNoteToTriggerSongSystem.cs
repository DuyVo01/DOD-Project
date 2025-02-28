using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class TraceNoteToTriggerSongSystem : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.IngamePlaying;

        ArchetypeStorage musicnNoteStorage;
        TransformComponent[] musicNoteTransforms;

        ArchetypeStorage worldStateStorage;
        WorldStateComponent[] worldStateComponents;

        EmptyEventChannel OnSongStartChannel;

        public TraceNoteToTriggerSongSystem(GlobalPoint globalPoint)
        {
            OnSongStartChannel = globalPoint.OnSongStartChannel;
        }

        public void RunCleanup() { }

        public void RunInitialize()
        {
            worldStateStorage = World.GetStorage(Archetype.Registry.WorldState);
            worldStateComponents = worldStateStorage.GetComponents<WorldStateComponent>();

            musicnNoteStorage = World.GetStorage(Archetype.Registry.MusicNote);
            musicNoteTransforms = musicnNoteStorage.GetComponents<TransformComponent>();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime)
        {
            if (
                musicNoteTransforms[0].Position.y
                <= worldStateComponents[0].FirstNotePositionToTriggerSong
            )
            {
                OnSongStartChannel.RaiseEvent(new EmptyData());
                IsEnabled = false;
            }
        }
    }
}
