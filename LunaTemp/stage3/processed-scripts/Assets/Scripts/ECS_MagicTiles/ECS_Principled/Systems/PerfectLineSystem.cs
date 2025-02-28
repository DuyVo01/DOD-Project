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

            //
            perfectLineSetting.portraitNormalizedPos.normalizedX.Subscribe(UpdatePerfectLinePos);
            perfectLineSetting.portraitNormalizedPos.normalizedY.Subscribe(UpdatePerfectLinePos);

            //
            perfectLineSetting.landscapeNormalizedPos.normalizedX.Subscribe(UpdatePerfectLinePos);
            perfectLineSetting.landscapeNormalizedPos.normalizedY.Subscribe(UpdatePerfectLinePos);

            //
            perfectLineSetting.portraitNormalizedSize.normalizedX.Subscribe(
                UpdatePerfectLineSizeData
            );
            perfectLineSetting.portraitNormalizedSize.normalizedY.Subscribe(
                UpdatePerfectLineSizeData
            );

            //
            perfectLineSetting.landscapeNormalizedSize.normalizedX.Subscribe(
                UpdatePerfectLineSizeData
            );
            perfectLineSetting.landscapeNormalizedSize.normalizedY.Subscribe(
                UpdatePerfectLineSizeData
            );

            OnOrientationChangedChannel.Subscribe(OnOrientationChanged);
        }

        public void RunCleanup() { }

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
                    perfectLineSetting.portraitNormalizedPos.normalizedX.Value,
                    perfectLineSetting.portraitNormalizedPos.normalizedY.Value
                );
            }
            else
            {
                perfectLineTransforms[0].Position = CameraViewUtils.GetPositionInCameraView(
                    mainCamera,
                    perfectLineSetting.landscapeNormalizedPos.normalizedX.Value,
                    perfectLineSetting.landscapeNormalizedPos.normalizedY.Value
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
                    perfectLineSetting.portraitNormalizedSize.normalizedX.Value,
                    perfectLineSetting.portraitNormalizedSize.normalizedY.Value,
                    false
                );
            }
            else
            {
                perfectLineTransforms[0].Size = SpriteUtility.ResizeInCameraView(
                    perfectLineSprite,
                    mainCamera,
                    perfectLineSetting.landscapeNormalizedSize.normalizedX.Value,
                    perfectLineSetting.landscapeNormalizedSize.normalizedY.Value,
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
