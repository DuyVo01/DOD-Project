using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class StartingNoteCreationSystem : GameSystemBase
    {
        private readonly MusicNoteCreationSetting musicNoteCreationSetting;
        private readonly StartingNoteSyncTool startingNoteSyncTool;

        // Track perfect line position for optimization
        private float lastPerfectLineTopLeftY;
        private float lastPerfectLineTopLeftX;

        // Track if the starting note has been set up
        private bool isInitialized = false;

        public StartingNoteCreationSystem(GlobalPoint globalPoint)
        {
            musicNoteCreationSetting = globalPoint.musicNoteCreationSettings;
            startingNoteSyncTool = globalPoint.startingNoteSyncTool;
        }

        protected override void Initialize()
        {
            // Initialize the sync tool
            startingNoteSyncTool.InitializeTool();

            // Initial setup on system initialization
            SetupStartingNote();
            isInitialized = true;
        }

        private void SetupStartingNote()
        {
            // Get references to the singleton components we need
            ref var startingNoteTransform = ref World.GetSingleton<
                StartingNoteTagComponent,
                TransformComponent
            >();
            ref var perfectLineCorner = ref World.GetSingleton<
                PerfectLineTagComponent,
                CornerComponent
            >();
            ref var perfectLine = ref World.GetSingleton<PerfectLineTagComponent>();
            ref var activeState = ref World.GetSingleton<
                StartingNoteTagComponent,
                ActiveStateComponent
            >();

            // Calculate lane width
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;
            float halfLaneWidth = laneWidth / 2f;

            // Calculate spawn position - first lane (index 0)
            float spawnX = perfectLineCorner.TopLeft.x + halfLaneWidth;
            float spawnY = perfectLineCorner.TopLeft.y;

            // Update starting note transform
            startingNoteTransform.Position = new Vector2(spawnX, spawnY);

            // Calculate scale based on perfect line properties
            float scaleX = perfectLine.PerfectLineWidth / 4;
            float scaleY = MagicTileHelper.CalculateScaleY(
                musicNoteCreationSetting.ShortNoteScaleYFactor,
                scaleX
            );

            startingNoteTransform.Size = new Vector2(scaleX, scaleY);

            // Sync the transform to the visual representation
            startingNoteSyncTool.SyncSingleton();

            // Store the current perfect line position for future reference
            lastPerfectLineTopLeftX = perfectLineCorner.TopLeft.x;
            lastPerfectLineTopLeftY = perfectLineCorner.TopLeft.y;

            Debug.Log("Starting note positioned at: " + startingNoteTransform.Position);
        }

        protected override void Execute(float deltaTime)
        {
            // Only run this check if we've been initialized
            if (!isInitialized)
                return;

            // Check if the perfect line has moved
            ref var perfectLineCorner = ref World.GetSingleton<
                PerfectLineTagComponent,
                CornerComponent
            >();

            // Reposition the starting note if the perfect line has moved
            if (
                perfectLineCorner.TopLeft.x != lastPerfectLineTopLeftX
                || perfectLineCorner.TopLeft.y != lastPerfectLineTopLeftY
            )
            {
                SetupStartingNote();
            }
        }

        /// <summary>
        /// Activates the starting note when the game begins
        /// </summary>
        public void ActivateStartingNote()
        {
            ref var activeState = ref World.GetSingleton<
                StartingNoteTagComponent,
                ActiveStateComponent
            >();
            activeState.IsActive = true;
            startingNoteSyncTool.SyncSingleton();
        }

        /// <summary>
        /// Deactivates the starting note when no longer needed
        /// </summary>
        public void DeactivateStartingNote()
        {
            ref var activeState = ref World.GetSingleton<
                StartingNoteTagComponent,
                ActiveStateComponent
            >();
            activeState.IsActive = false;
            startingNoteSyncTool.SyncSingleton();
        }
    }
}
