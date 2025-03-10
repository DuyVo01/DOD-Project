using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;
using static ECS_MagicTile.DelegateTypes;

namespace ECS_MagicTile
{
    public class LaneLineSystem : GameSystemBase
    {
        private BoolEventChannel onOrientationChangedChannel;
        private Camera targetCamera;
        private LaneLineSyncTool laneLineSyncTool;
        private LaneLineSettings laneLineSettings;
        private int eventListenerId;

        // References to component data

        public LaneLineSystem(GlobalPoint globalPoint)
        {
            onOrientationChangedChannel = globalPoint.OnOrientationChangedChannel;
            laneLineSyncTool = globalPoint.laneLineSyncTool;
            laneLineSettings = globalPoint.laneLineSettings;
            targetCamera = globalPoint.mainCamera;
        }

        protected override void Initialize()
        {
            CreateLaneLines();
            laneLineSyncTool.InitializeTool();

            AdjustLaneLines();

            eventListenerId = onOrientationChangedChannel.Subscribe(
                target: this,
                (target, data) => OnOrientationChanged(data)
            );
        }

        protected override void Execute(float deltaTime)
        {
            // Nothing needed in update for this system
        }

        protected override void Cleanup()
        {
            onOrientationChangedChannel.Unsubscribe(eventListenerId);
        }

        private void CreateLaneLines()
        {
            for (int i = 0; i < laneLineSettings.laneLineCount; i++)
            {
                World.CreateEntity(new TransformComponent(), new LaneLineTagComponent());
            }
        }

        private void OnOrientationChanged(bool isPortrait)
        {
            AdjustLaneLines();
        }

        private void AdjustLaneLines()
        {
            CornerComponent perfectLineCorner = World.GetSingleton<
                PerfectLineTagComponent,
                CornerComponent
            >();
            if (perfectLineCorner.TopLeft.x == 0 && perfectLineCorner.TopLeft.y == 0)
            {
                Debug.LogWarning(
                    "Perfect line corner not initialized yet, can't adjust lane lines"
                );
                return;
            }

            // Calculate lane width once
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;

            float spawnY = CameraViewUtils.GetPositionYInCameraView(targetCamera, .5f);

            int index = 0;

            // Get a direct reference to the component
            World
                .CreateQuery()
                .ForEach(
                    (
                        ref TransformComponent transform,
                        ref LaneLineTagComponent tag,
                        int entityId
                    ) =>
                    {
                        float spawnX = index * laneWidth + perfectLineCorner.TopLeft.x;
                        transform.Position = new Vector3(spawnX, spawnY, 0);
                        transform.Size = SpriteUtility.ResizeInCameraView(
                            laneLineSyncTool.GetSpriteAtIndex(index),
                            targetCamera,
                            laneLineSettings.laneLineWidth,
                            1,
                            false
                        );

                        laneLineSyncTool.SyncLaneLineTransform(index, transform);
                        index++;
                    }
                );
        }
    }
}
