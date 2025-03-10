using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class LaneLineSyncTool : BaseSyncTool
    {
        protected override Archetype Archetype => Archetype.Registry.LaneLines;

        private readonly EntityViewFactory laneLineViewFactory;
        private SpriteRenderer[] laneLineSprites;

        private int laneLineCount;

        public LaneLineSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
        {
            laneLineViewFactory = new EntityViewFactory(
                globalPoint.laneLineSettings.landLinePrefab,
                null
            );

            laneLineCount = globalPoint.laneLineSettings.laneLineCount;
        }

        public override void InitializeTool()
        {
            base.InitializeTool();
            laneLineSprites = new SpriteRenderer[laneLineCount];

            for (int i = 0; i < laneLineCount; i++)
            {
                laneLineSprites[i] = GetOrCreateLaneLineView(i).GetComponent<SpriteRenderer>();
            }
        }

        public void SyncLaneLineTransform(int index, in TransformComponent lanelineTransform)
        {
            laneLineSprites[index].transform.position = lanelineTransform.Position;
            laneLineSprites[index].transform.localScale = lanelineTransform.Size;
        }

        public SpriteRenderer GetSpriteAtIndex(int index)
        {
            return laneLineSprites[index];
        }

        private GameObject GetOrCreateLaneLineView(int entityId)
        {
            return laneLineViewFactory.GetOrCreateView(entityId);
        }
    }
}
