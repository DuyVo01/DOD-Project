using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteSystem : IGameSystem
    {
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.All;

        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly BoolEventChannel OnOrientationChangedChannel;

        private ArchetypeStorage startingNoteStorage;
        private ArchetypeStorage perfectLineStorage;

        public StartingNoteSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            OnOrientationChangedChannel = globalPoint.OnOrientationChangedChannel;
        }

        public void Cleanup() { }

        public void Initialize()
        {
            startingNoteStorage ??= World.GetStorage(Archetype.Registry.StartingNote);
            perfectLineStorage ??= World.GetStorage(Archetype.Registry.PerfectLine);

            SetupStartingNote();

            OnOrientationChangedChannel.Subscribe(OnOrientationChanged);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime) { }

        private void OnOrientationChanged(bool isPortrait)
        {
            SetupStartingNote();
        }

        private void SetupStartingNote()
        {
            ref ActiveStateComponent activeState =
                ref startingNoteStorage.GetComponents<ActiveStateComponent>()[0];
            ref TransformComponent transform =
                ref startingNoteStorage.GetComponents<TransformComponent>()[0];

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
    }
}
