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
    }
}
