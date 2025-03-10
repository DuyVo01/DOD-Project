using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class PerfectLineSyncTool : BaseSyncTool
    {
        protected override Archetype Archetype => Archetype.Registry.PerfectLine;
        private SpriteRenderer perfectLineSprite;

        public PerfectLineSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
        {
            perfectLineSprite = globalPoint.perfectLineObject.GetComponent<SpriteRenderer>();
        }

        public void SyncPerfectLineTransform(TransformComponent perfectLineTransform)
        {
            perfectLineSprite.transform.position = perfectLineTransform.Position;
            perfectLineSprite.transform.localScale = perfectLineTransform.Size;
        }
        
        /// <summary>
        /// Syncs the perfect line using the new singleton API with direct references
        /// </summary>
        public void SyncSingleton()
        {
            // Get components individually
            ref var transform = ref World.GetSingleton<PerfectLineTagComponent, TransformComponent>();
            ref var perfectLine = ref World.GetSingleton<PerfectLineTagComponent, PerfectLineTagComponent>();
            
            // Update the visuals using these references
            perfectLineSprite.transform.position = transform.Position;
            perfectLineSprite.transform.localScale = new Vector3(perfectLine.PerfectLineWidth, 0.1f, 1);
            
            // Demonstrate that we can modify the singleton directly through the reference
            // For example, we might want to ensure the width never gets too small
            if (perfectLine.PerfectLineWidth < 0.5f)
            {
                perfectLine.PerfectLineWidth = 0.5f;
                // No need to update the component back to the singleton manager
            }
        }
    }
}
