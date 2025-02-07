using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class SingletonCreationSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.WaitingToStart;

        private readonly PerfectLineSettingSO perfectLineSettingSO;
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;

        public SingletonCreationSystem(GlobalPoint globalPoint)
        {
            this.perfectLineSettingSO = globalPoint.perfectLineSettingSO;
            this.musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
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

            ArchetypeStorage perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            ref CornerComponent perfectLineCorner =
                ref perfectLineStorage.GetComponents<CornerComponent>()[0];

            PerfectLine.PerfectLineWidth = perfectLineSettingSO.PerfectLineWidth();
            perfectLineCorner.TopLeft = perfectLineSettingSO.TopLeft;
            perfectLineCorner.TopRight = perfectLineSettingSO.TopRight;
            perfectLineCorner.BottomLeft = perfectLineSettingSO.BottomLeft;
            perfectLineCorner.BottomRight = perfectLineSettingSO.BottomRight;
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

            ArchetypeStorage storage = World.GetStorage(Archetype.Registry.StartingNote);
            ArchetypeStorage perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);

            ref ActiveStateComponent activeState =
                ref storage.GetComponents<ActiveStateComponent>()[0];
            ref TransformComponent transform = ref storage.GetComponents<TransformComponent>()[0];

            ref CornerComponent perfectLineCorners =
                ref perfectLineStorage.GetComponents<CornerComponent>()[0];

            // Calculate lane width
            float totalWidth = perfectLineCorners.TopRight.x - perfectLineCorners.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            activeState.isActive = true;

            // Calculate spawn position
            float spawnX = perfectLineCorners.TopLeft.x + (0 * laneWidth) + halfLaneWidth;
            float spawnY = perfectLineCorners.TopLeft.y;

            transform.Posision = new Vector2(spawnX, spawnY);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            float scaleX = PerfectLine.PerfectLineWidth / 4;

            float scaleY = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.ShortNoteScaleYFactor,
                scaleX
            );

            transform.Size = new Vector2(scaleX, scaleY);
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
