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

        private ArchetypeStorage perfectLineStorage;

        private SpriteRenderer perfectLineSprite;

        public SingletonCreationSystem(GlobalPoint globalPoint)
        {
            this.musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
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
            var components = new object[] { new PerfectLineTagComponent(), new CornerComponent() };
            World.CreateEntityWithComponents(Archetype.Registry.PerfectLine, components);

            perfectLineStorage ??= World.GetStorage(Archetype.Registry.PerfectLine);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            ref CornerComponent perfectLineCorner =
                ref perfectLineStorage.GetComponents<CornerComponent>()[0];

            SpriteUtility.SpriteCorners spriteCorners = SpriteUtility.GetSpriteCorners(
                perfectLineSprite
            );

            PerfectLine.PerfectLineWidth = Mathf.Abs(
                spriteCorners.TopLeft.x - spriteCorners.TopRight.x
            );
            perfectLineCorner.TopLeft = spriteCorners.TopLeft;
            perfectLineCorner.TopRight = spriteCorners.TopRight;
            perfectLineCorner.BottomLeft = spriteCorners.BottomLeft;
            perfectLineCorner.BottomRight = spriteCorners.BottomRight;
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
