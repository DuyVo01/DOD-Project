using ECS_MagicTile.Components;
using UnityEngine;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    public class SingletonCreationSystem : GameSystemBase
    {
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly PerfectLineSetting perfectLineSetting;

        public SingletonCreationSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            perfectLineSetting = globalPoint.perfectLineSetting;
        }

        protected override void Initialize()
        {
            CreatePerfectLine();
            CreateStartingNote();
            CreateGameScore();
            CreateProgress();
            CreateWorldStateComponent();

            Debug.Log("Singletons created with optimized storage");
        }

        protected override void Execute(float deltaTime)
        {
            // No per-frame logic needed
        }

        protected override void Cleanup()
        {
            // No cleanup needed
        }

        private void CreatePerfectLine()
        {
            // Create the perfect line singleton directly with components
            // The ISingletonFlag implementation will route this to optimized storage
            World.CreateEntity(
                new TransformComponent
                {
                    Position = perfectLineSetting.Position,
                    Size = perfectLineSetting.Size,
                },
                new PerfectLineTagComponent { PerfectLineWidth = perfectLineSetting.Width },
                new CornerComponent()
            );

            Debug.Log(
                $"Perfect Line singleton created with ID: {World.GetSingletonEntityId<PerfectLineTagComponent>()}"
            );
        }

        private void CreateStartingNote()
        {
            // Create the starting note singleton directly with components
            // The ISingletonFlag implementation will route this to optimized storage
            World.CreateEntity(
                new TransformComponent { Position = new Vector2(0, 0), Size = new Vector2(1, 1) },
                new ActiveStateComponent { IsActive = true },
                new StartingNoteTagComponent { initalLane = 0 }
            );

            Debug.Log(
                $"Starting Note singleton created with ID: {World.GetSingletonEntityId<StartingNoteTagComponent>()}"
            );
        }

        private void CreateGameScore()
        {
            // Create the game score singleton
            // Note: This would need to be updated with ISingletonFlag to use the singleton storage
            World.CreateEntity(new ScoreComponent { TotalScore = 0 });
        }

        private void CreateProgress()
        {
            // Parse the MIDI data
            MusicNoteMidiData musicNoteMidiData = MidiNoteParser.ParseFromText(
                musicNoteCreationSetting.MidiContent.text
            );

            // Create the progress singleton
            // Note: This would need to be updated with ISingletonFlag to use the singleton storage
            World.CreateEntity(
                new ProgressComponent
                {
                    CurrentProgressRawValue = 0,
                    currentProgressPercent = 0,
                    MaxProgressRawValue = musicNoteMidiData.TotalNotes,
                }
            );
        }

        private void CreateWorldStateComponent()
        {
            // Create the world state singleton
            // Note: This would need to be updated with ISingletonFlag to use the singleton storage
            World.CreateEntity(new WorldStateComponent());
        }
    }
}
