using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class LaneLineSyncTool : BaseSyncTool
    {
        public LaneLineSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
        {
            laneLineViewFactory = new EntityViewFactory(
                globalPoint.laneLineSettings.landLinePrefab,
                null
            );
        }

        protected override Archetype Archetype => Archetype.Registry.LaneLines;

        public SpriteRenderer[] LaneLineSprites => laneLineSprites;

        private readonly EntityViewFactory laneLineViewFactory;
        private SpriteRenderer[] laneLineSprites;

        public override void InitializeTool()
        {
            base.InitializeTool();
            laneLineSprites = new SpriteRenderer[DedicatedStorage.Count];

            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                int entityId = DedicatedStorage.EntityIds[i];
                laneLineSprites[i] = GetOrCreateLaneLineView(entityId)
                    .GetComponent<SpriteRenderer>();
            }
        }

        public void SyncLaneLineTransform(TransformComponent[] lanelineTransform)
        {
            for (int i = 0; i < DedicatedStorage.Count; i++)
            {
                laneLineSprites[i].transform.position = lanelineTransform[i].Position;
                laneLineSprites[i].transform.localScale = lanelineTransform[i].Size;
            }
        }

        private GameObject GetOrCreateLaneLineView(int entityId)
        {
            return laneLineViewFactory.GetOrCreateView(entityId);
        }
    }
}
