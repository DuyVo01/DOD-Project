using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class LaneLineSystem : IGameSystem
    {
        public LaneLineSystem(GlobalPoint globalPoint)
        {
            onOrientationChangedChannel = globalPoint.OnOrientationChangedChannel;
            laneLineSyncTool = globalPoint.laneLineSyncTool;
            laneLineSettings = globalPoint.laneLineSettings;
            targetCamera = globalPoint.mainCamera;
        }

        public bool IsEnabled { get; set; }
        public World World { get; set; }

        private BoolEventChannel onOrientationChangedChannel;
        private Camera targetCamera;

        public EGameState GameStateToExecute => EGameState.IngamePrestart;

        ArchetypeStorage perfectLineStorage;
        CornerComponent[] perfectLineCorners;

        ArchetypeStorage laneLineStorage;
        TransformComponent[] laneLineTransforms;

        private LaneLineSyncTool laneLineSyncTool;
        private LaneLineSettings laneLineSettings;

        private int eventListenerId;

        public void RunCleanup()
        {
            onOrientationChangedChannel.Unsubscribe(eventListenerId);
        }

        public void RunInitialize()
        {
            CreateLaneLines();
            laneLineSyncTool.InitializeTool();
            perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);
            laneLineStorage = World.GetStorage(Archetype.Registry.LaneLines);

            laneLineTransforms = laneLineStorage.GetComponents<TransformComponent>();
            perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>();

            AdjustLaneLines();

            eventListenerId = onOrientationChangedChannel.Subscribe(
                target: this,
                (target, data) => OnOrientationChanged(data)
            );
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime) { }

        private void CreateLaneLines()
        {
            for (int i = 0; i < 5; i++)
            {
                var LaneLinesComponents = new object[] { new TransformComponent() };
                World.CreateEntityWithComponents(Archetype.Registry.LaneLines, LaneLinesComponents);
            }
        }

        private void OnOrientationChanged(bool isPortrait)
        {
            AdjustLaneLines();
        }

        private void OnLaneLineSettingsAdjustInInpector(float value)
        {
            AdjustLaneLines();
        }

        private void AdjustLaneLines()
        {
            ref CornerComponent perfectLineCorner = ref perfectLineCorners[0];

            // Calculate lane width once
            float totalWidth = perfectLineCorner.TopRight.x - perfectLineCorner.TopLeft.x;
            float laneWidth = totalWidth / 4;

            for (int i = 0; i < laneLineStorage.Count; i++)
            {
                float spawnX = i * laneWidth + perfectLineCorner.TopLeft.x;
                float spawnY = CameraViewUtils.GetPositionYInCameraView(targetCamera, .5f);

                laneLineTransforms[i].Position = new Vector3(spawnX, spawnY, 0);
                laneLineTransforms[i].Size = SpriteUtility.ResizeInCameraView(
                    laneLineSyncTool.LaneLineSprites[i],
                    targetCamera,
                    laneLineSettings.laneLineWidth,
                    1,
                    false
                );
            }

            laneLineSyncTool.SyncLaneLineTransform(laneLineTransforms);
        }
    }
}
