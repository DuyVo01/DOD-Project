using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteCreationSystem : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        private readonly MusicNoteCreationSetting musicNoteCreationSetting;

        private readonly StartingNoteSyncTool startingNoteSyncTool;

        private ArchetypeStorage startingNoteStorage;
        private ArchetypeStorage perfectLineStorage;

        private TransformComponent[] startingNoteTransforms;
        private CornerComponent[] perfectLineCorners;

        float lastPerfectLineTopLeftY;
        float lastPerfectLineTopLeftX;

        public StartingNoteCreationSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
        }

        public void RunCleanup() { }

        public void RunInitialize()
        {
            startingNoteSyncTool.InitializeTool();

            startingNoteStorage ??= World.GetStorage(Archetype.Registry.StartingNote);
            perfectLineStorage ??= World.GetStorage(Archetype.Registry.PerfectLine);

            perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>();
            startingNoteTransforms = startingNoteStorage.GetComponents<TransformComponent>();

            ref ActiveStateComponent activeState =
                ref startingNoteStorage.GetComponents<ActiveStateComponent>()[0];
            activeState.isActive = true;

            SetupStartingNote();
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime)
        {
            if (
                lastPerfectLineTopLeftY != perfectLineCorners[0].TopLeft.y
                || lastPerfectLineTopLeftX != perfectLineCorners[0].TopLeft.x
            )
            {
                lastPerfectLineTopLeftY = perfectLineCorners[0].TopLeft.y;
                lastPerfectLineTopLeftX = perfectLineCorners[0].TopLeft.x;
                SetupStartingNote();
            }

            //Debug.Log($"PerfectLine Topleft: {perfectLineCorners[0].TopLeft.x}");
        }

        private void SetupStartingNote()
        {
            ref TransformComponent transform = ref startingNoteTransforms[0];

            ref CornerComponent perfectLineCorner = ref perfectLineCorners[0];

            // Calculate lane width
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            // Calculate spawn position
            float spawnX = perfectLineCorner.TopLeft.x + (0 * laneWidth) + halfLaneWidth;
            float spawnY = perfectLineCorner.TopLeft.y;

            transform.Position = new Vector2(spawnX, spawnY);

            ref PerfectLineTagComponent PerfectLine =
                ref perfectLineStorage.GetComponents<PerfectLineTagComponent>()[0];

            float scaleX = PerfectLine.PerfectLineWidth / 4;

            float scaleY = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.ShortNoteScaleYFactor,
                scaleX
            );

            transform.Size = new Vector2(scaleX, scaleY);

            startingNoteSyncTool.SyncStartNoteTransform(transform);
        }
    }
}
