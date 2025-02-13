using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class SingletonCreationSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.WaitingToStart;

        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly PerfectLineSetting perfectLineSetting;

        private ArchetypeStorage perfectLineStorage;

        private SpriteRenderer perfectLineSprite;

        public SingletonCreationSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            perfectLineSetting = globalPoint.perfectLineSetting;

            perfectLineSprite = globalPoint.perfectLineObject.GetComponent<SpriteRenderer>();
        }

        public void Cleanup()
        {
            //
        }

        public void Initialize()
        {
            CreatePerfectLine();
            CreateStartingNote();
            CreateGameScore();
            CreateProgress();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
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
    }
}
