using ECS_MagicTile.Components;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class PerfectLineSystem : IGameSystem
    {
        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        //Storage and Data arrays
        ArchetypeStorage perfectLineStorage;
        PerfectLineTagComponent[] perfectLineTagComponents;
        CornerComponent[] perfectLineCorners;
        TransformComponent[] perfectLineTransforms;

        //SO settings
        PerfectLineSetting perfectLineSetting;

        //Event Channels
        private BoolEventChannel OnOrientationChangedChannel;

        //Game object refs
        SpriteRenderer perfectLineSprite;
        Camera mainCamera;

        SpriteUtility.SpriteCorners perfectLineCornersInCamSpace;

        private PerfectLineSyncTool perfectLineSyncTool;

        private int eventListenerId;

        public PerfectLineSystem(GlobalPoint globalPoint)
        {
            perfectLineSprite = globalPoint.perfectLineObject.GetComponent<SpriteRenderer>();
            perfectLineSetting = globalPoint.perfectLineSetting;
            mainCamera = globalPoint.mainCamera;
            OnOrientationChangedChannel = globalPoint.OnOrientationChangedChannel;

            perfectLineSyncTool = globalPoint.perfectLineSyncTool;
        }

        public EGameState GameStateToExecute => EGameState.All;

        public void RunInitialize()
        {
            perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);

            perfectLineTagComponents = perfectLineStorage.GetComponents<PerfectLineTagComponent>();
            perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>();
            perfectLineTransforms = perfectLineStorage.GetComponents<TransformComponent>();

            eventListenerId = OnOrientationChangedChannel.Subscribe(
                target: this,
                (target, data) => OnOrientationChanged(data)
            );
        }

        public void RunCleanup()
        {
            OnOrientationChangedChannel.Unsubscribe(eventListenerId);
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime) { }

        private void UpdatePerfectLinePos(float value)
        {
            if (ScreenManager.Instance.IsPortrait)
            {
                perfectLineTransforms[0].Position = CameraViewUtils.GetPositionInCameraView(
                    mainCamera,
                    perfectLineSetting.portraitNormalizedPos.x,
                    perfectLineSetting.portraitNormalizedPos.y
                );
            }
            else
            {
                perfectLineTransforms[0].Position = CameraViewUtils.GetPositionInCameraView(
                    mainCamera,
                    perfectLineSetting.landscapeNormalizedPos.x,
                    perfectLineSetting.landscapeNormalizedPos.y
                );
            }

            perfectLineSyncTool.SyncPerfectLineTransform(perfectLineTransforms[0]);

            UpdatePerfectLineCornersData();
        }

        private void UpdatePerfectLineSizeData(float value)
        {
            if (ScreenManager.Instance.IsPortrait)
            {
                perfectLineTransforms[0].Size = SpriteUtility.ResizeInCameraView(
                    perfectLineSprite,
                    mainCamera,
                    perfectLineSetting.portraitNormalizedSize.x,
                    perfectLineSetting.portraitNormalizedSize.y,
                    false
                );
            }
            else
            {
                perfectLineTransforms[0].Size = SpriteUtility.ResizeInCameraView(
                    perfectLineSprite,
                    mainCamera,
                    perfectLineSetting.landscapeNormalizedSize.x,
                    perfectLineSetting.landscapeNormalizedSize.y,
                    false
                );
            }

            perfectLineSyncTool.SyncPerfectLineTransform(perfectLineTransforms[0]);

            UpdatePerfectLineCornersData();
        }

        private void UpdatePerfectLineCornersData()
        {
            perfectLineCornersInCamSpace = SpriteUtility.GetSpriteCorners(perfectLineSprite);

            perfectLineCorners[0].TopLeft = perfectLineCornersInCamSpace.TopLeft;
            perfectLineCorners[0].TopRight = perfectLineCornersInCamSpace.TopRight;
            perfectLineCorners[0].BottomLeft = perfectLineCornersInCamSpace.BottomLeft;
            perfectLineCorners[0].BottomRight = perfectLineCornersInCamSpace.BottomRight;

            perfectLineTagComponents[0].PerfectLineWidth = Mathf.Abs(
                perfectLineCornersInCamSpace.TopLeft.x - perfectLineCornersInCamSpace.TopRight.x
            );
        }

        private void OnOrientationChanged(bool isPortrait)
        {
            if (isPortrait)
            {
                UpdatePerfectLinePos(0f);
            }
            else
            {
                UpdatePerfectLinePos(0f);
            }
            UpdatePerfectLineSizeData(0f);
        }
    }
}
