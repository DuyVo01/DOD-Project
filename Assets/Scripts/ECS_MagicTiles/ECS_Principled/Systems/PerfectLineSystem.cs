using ECS_MagicTile.Components;
using EventChannel;
using Unity.Mathematics;
using Unity.VisualScripting;
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

        float lastNormalizeYInCamSpacePortrait;
        float lastNormalizeYInCamSpaceLandscape;
        SpriteUtility.SpriteCorners perfectLineCornersInCamSpace;

        public PerfectLineSystem(GlobalPoint globalPoint)
        {
            perfectLineSprite = globalPoint.perfectLineObject.GetComponent<SpriteRenderer>();
            perfectLineSetting = globalPoint.perfectLineSetting;
            mainCamera = globalPoint.mainCamera;
            OnOrientationChangedChannel = globalPoint.OnOrientationChangedChannel;
        }

        public EGameState GameStateToExecute => EGameState.All;

        public void Initialize()
        {
            perfectLineStorage = World.GetStorage(Archetype.Registry.PerfectLine);
            perfectLineTagComponents = perfectLineStorage.GetComponents<PerfectLineTagComponent>();
            perfectLineCorners = perfectLineStorage.GetComponents<CornerComponent>();
            perfectLineTransforms = perfectLineStorage.GetComponents<TransformComponent>();

            UpdatePerfectLineData(true);
            UpdatePerfectLineData(false);

            OnOrientationChangedChannel.Subscribe(UpdatePerfectLineData);
        }

        public void Cleanup() { }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void Update(float deltaTime)
        {
            if (
                perfectLineSetting.portraitNormalizedPos.normalizedY
                != lastNormalizeYInCamSpacePortrait
            )
            {
                lastNormalizeYInCamSpacePortrait = perfectLineSetting
                    .portraitNormalizedPos
                    .normalizedY;

                UpdatePerfectLineData(
                    Screen.currentResolution.height > Screen.currentResolution.width
                );

                Debug.Log($"PerfectLine Topleft: {perfectLineCorners[0].TopLeft.x}");

                return;
            }

            if (
                perfectLineSetting.landscapeNormalizedPos.normalizedY
                != lastNormalizeYInCamSpaceLandscape
            )
            {
                lastNormalizeYInCamSpaceLandscape = perfectLineSetting
                    .landscapeNormalizedPos
                    .normalizedY;

                UpdatePerfectLineData(
                    Screen.currentResolution.height > Screen.currentResolution.width
                );

                return;
            }
        }

        private void UpdatePerfectLineData(bool isPortrait)
        {
            if (isPortrait)
            {
                perfectLineTransforms[0].Posision = CameraViewUtils.GetPositionInCameraView(
                    mainCamera,
                    perfectLineSetting.portraitNormalizedPos.normalizedX,
                    perfectLineSetting.portraitNormalizedPos.normalizedY
                );
            }
            else
            {
                perfectLineTransforms[0].Posision = CameraViewUtils.GetPositionInCameraView(
                    mainCamera,
                    perfectLineSetting.landscapeNormalizedPos.normalizedX,
                    perfectLineSetting.landscapeNormalizedPos.normalizedY
                );
            }

            perfectLineCornersInCamSpace = SpriteUtility.GetSpriteCorners(perfectLineSprite);

            perfectLineCorners[0].TopLeft = perfectLineCornersInCamSpace.TopLeft;
            perfectLineCorners[0].TopRight = perfectLineCornersInCamSpace.TopRight;
            perfectLineCorners[0].BottomLeft = perfectLineCornersInCamSpace.BottomLeft;
            perfectLineCorners[0].BottomRight = perfectLineCornersInCamSpace.BottomRight;

            perfectLineTagComponents[0].PerfectLineWidth = Mathf.Abs(
                perfectLineCornersInCamSpace.TopLeft.x - perfectLineCornersInCamSpace.TopRight.x
            );
        }
    }
}
