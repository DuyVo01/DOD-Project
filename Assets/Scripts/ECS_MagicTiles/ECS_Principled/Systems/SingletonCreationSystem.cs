using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class SingletonCreationSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.IngamePrestart;

        private readonly MusicNoteCreationSetting musicNoteCreationSetting;

        public SingletonCreationSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
        }

        public void RunCleanup()
        {
            //
        }

        public void RunInitialize()
        {
            CreatePerfectLine();
            CreateStartingNote();
            CreateGameScore();
            CreateProgress();
            CreateWorldStateComponent();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime)
        {
            //
        }

        private void CreatePerfectLine()
        {
            //The perfect line singleton
            var components = new object[]
            {
                new TransformComponent(),
                new PerfectLineTagComponent(),
                new CornerComponent(),
            };
            World.CreateEntityWithComponents(Archetype.Registry.PerfectLine, components);
        }

        private void CreateStartingNote()
        {
            var components = new object[]
            {
                new TransformComponent(),
                new ActiveStateComponent(),
                new StartingNoteTagComponent(),
            };
            World.CreateEntityWithComponents(Archetype.Registry.StartingNote, components);
        }

        private void CreateGameScore()
        {
            var components = new object[] { new ScoreComponent { TotalScore = 0 } };
            World.CreateEntityWithComponents(Archetype.Registry.GameScore, components);
        }

        private void CreateProgress()
        {
            MusicNoteMidiData musicNoteMidiData = MidiNoteParser.ParseFromText(
                musicNoteCreationSetting.MidiContent.text
            );
            var components = new object[]
            {
                new ProgressComponent
                {
                    CurrentProgressRawValue = 0,
                    currentProgressPercent = 0,
                    MaxProgressRawValue = musicNoteMidiData.TotalNotes,
                },
            };
            World.CreateEntityWithComponents(Archetype.Registry.SongProgress, components);
        }

        private void CreateWorldStateComponent()
        {
            var components = new object[] { new WorldStateComponent() };
            World.CreateEntityWithComponents(Archetype.Registry.WorldState, components);
        }
    }
}
