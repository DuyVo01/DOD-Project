using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    /// <summary>
    /// System that manages the perfect line position, size, and corners
    /// </summary>
    public class PerfectLineSystem : GameSystemBase
    {
        private readonly PerfectLineSetting perfectLineSetting;
        private readonly PerfectLineSyncTool perfectLineSyncTool;

        // Track time for any animations
        private float elapsedTime = 0;

        public PerfectLineSystem(GlobalPoint globalPoint)
        {
            perfectLineSetting = globalPoint.perfectLineSetting;
            perfectLineSyncTool = globalPoint.perfectLineSyncTool;
        }

        protected override void Initialize()
        {
            Debug.Log("PerfectLineSystem initialized with singleton API");
            UpdatePerfectLine();
        }

        protected override void Execute(float deltaTime)
        {
            // elapsedTime += deltaTime;

            // // Check if we need to update the perfect line components
            // // This could be based on game events, difficulty changes, etc.
            // bool needsUpdate = false;

            // // Add any conditions here that would require an update
            // // For example, if the perfectLineSetting has changed

            // if (needsUpdate)
            // {
            //     UpdatePerfectLine();
            // }

            // // Sync visual representation
            // perfectLineSyncTool.SyncSingleton();
        }

        /// <summary>
        /// Updates all components of the perfect line
        /// </summary>
        private void UpdatePerfectLine()
        {
            // Get references to the singleton components
            ref var transform = ref World.GetSingleton<
                PerfectLineTagComponent,
                TransformComponent
            >();
            ref var perfectLine = ref World.GetSingleton<PerfectLineTagComponent>();
            ref var corner = ref World.GetSingleton<PerfectLineTagComponent, CornerComponent>();

            // Update transform based on settings
            transform.Position = perfectLineSetting.Position;
            transform.Size = perfectLineSetting.Size;

            // Update the tag component

            // The most important part: Calculate and update the corner positions
            // These are used for collision detection and positioning other elements

            perfectLine.PerfectLineWidth = perfectLineSetting.Width;

            // Extract values for clarity
            float width = perfectLine.PerfectLineWidth;
            float height = 0.1f; // Typical height for perfect line
            Vector2 position = transform.Position;

            // Calculate half extents
            float halfWidth = width / 2f;
            float halfHeight = height / 2f;

            // Update corner positions
            corner.TopLeft = new Vector2(position.x - halfWidth, position.y + halfHeight);
            corner.TopRight = new Vector2(position.x + halfWidth, position.y + halfHeight);
            corner.BottomLeft = new Vector2(position.x - halfWidth, position.y - halfHeight);
            corner.BottomRight = new Vector2(position.x + halfWidth, position.y - halfHeight);

            Debug.Log($"Updated perfect line corners at position {position}, width {width}");
        }

        /// <summary>
        /// Called externally when settings are changed
        /// </summary>
        public void OnSettingsChanged()
        {
            UpdatePerfectLine();
        }
    }
}
