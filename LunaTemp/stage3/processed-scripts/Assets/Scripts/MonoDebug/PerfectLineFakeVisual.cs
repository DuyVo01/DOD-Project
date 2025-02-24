using ECS_MagicTile;
using EventChannel;
using UnityEngine;

public class PerfectLineFakeVisual : MonoBehaviour
{
    [SerializeField]
    private PerfectLineSetting perfectLineSetting;

    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private BoolEventChannel onOrientationChangedChannel;

    private SpriteRenderer perfectLineVisualSprite;

    void Start()
    {
        perfectLineVisualSprite = GetComponent<SpriteRenderer>();
        UpdatePerfectLineFakeVisualSize(0);
    }

    void OnEnable()
    {
        //Landscape Normalize Size
        perfectLineSetting.landscapeNormalizedSize.normalizedX.Subscribe(
            UpdatePerfectLineFakeVisualSize
        );
        perfectLineSetting.landscapeNormalizedSize.normalizedY.Subscribe(
            UpdatePerfectLineFakeVisualSize
        );

        //Portrait Normalize Size
        perfectLineSetting.portraitNormalizedSize.normalizedX.Subscribe(
            UpdatePerfectLineFakeVisualSize
        );
        perfectLineSetting.portraitNormalizedSize.normalizedY.Subscribe(
            UpdatePerfectLineFakeVisualSize
        );

        //Potrait Normalized Pos
        perfectLineSetting.portraitNormalizedPos.normalizedX.Subscribe(
            UpdatePerfectLineFakeVisualPosition
        );
        perfectLineSetting.portraitNormalizedPos.normalizedY.Subscribe(
            UpdatePerfectLineFakeVisualPosition
        );

        //Landscape Normalized Pos
        perfectLineSetting.landscapeNormalizedPos.normalizedX.Subscribe(
            UpdatePerfectLineFakeVisualPosition
        );
        perfectLineSetting.landscapeNormalizedPos.normalizedY.Subscribe(
            UpdatePerfectLineFakeVisualPosition
        );

        onOrientationChangedChannel.Subscribe(UpdatePosition);
        onOrientationChangedChannel.Subscribe(UpdateSize);
    }

    void OnDisable()
    {
        perfectLineSetting.landscapeNormalizedSize.normalizedX.Unsubscribe(
            UpdatePerfectLineFakeVisualSize
        );
        perfectLineSetting.landscapeNormalizedSize.normalizedY.Unsubscribe(
            UpdatePerfectLineFakeVisualSize
        );
        perfectLineSetting.portraitNormalizedSize.normalizedX.Unsubscribe(
            UpdatePerfectLineFakeVisualSize
        );
        perfectLineSetting.portraitNormalizedSize.normalizedY.Unsubscribe(
            UpdatePerfectLineFakeVisualSize
        );

        perfectLineSetting.portraitNormalizedPos.normalizedX.Unsubscribe(
            UpdatePerfectLineFakeVisualPosition
        );
        perfectLineSetting.portraitNormalizedPos.normalizedY.Unsubscribe(
            UpdatePerfectLineFakeVisualPosition
        );
        perfectLineSetting.landscapeNormalizedPos.normalizedX.Unsubscribe(
            UpdatePerfectLineFakeVisualPosition
        );
        perfectLineSetting.landscapeNormalizedPos.normalizedY.Unsubscribe(
            UpdatePerfectLineFakeVisualPosition
        );

        onOrientationChangedChannel.Unsubscribe(UpdatePosition);
        onOrientationChangedChannel.Unsubscribe(UpdateSize);
    }

    private void UpdatePerfectLineFakeVisualSize(float value)
    {
        if (ScreenManager.Instance.IsPortrait)
        {
            UpdateSize(true);
        }
        else
        {
            UpdateSize(false);
        }
    }

    private void UpdatePerfectLineFakeVisualPosition(float value)
    {
        if (ScreenManager.Instance.IsPortrait)
        {
            UpdatePosition(true);
        }
        else
        {
            UpdatePosition(false);
        }
    }

    private void UpdatePosition(bool isPortrait)
    {
        if (isPortrait)
        {
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                perfectLineSetting.portraitNormalizedPos.normalizedX.Value,
                perfectLineSetting.portraitNormalizedPos.normalizedY.Value
            );
        }
        else
        {
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                perfectLineSetting.landscapeNormalizedPos.normalizedX.Value,
                perfectLineSetting.landscapeNormalizedPos.normalizedY.Value
            );
        }
    }

    private void UpdateSize(bool isPortrait)
    {
        if (isPortrait)
        {
            perfectLineVisualSprite.ResizeInCameraView(
                targetCamera,
                1,
                perfectLineSetting.portraitNormalizedSize.normalizedY.Value,
                false
            );
        }
        else
        {
            perfectLineVisualSprite.ResizeInCameraView(
                targetCamera,
                1,
                perfectLineSetting.landscapeNormalizedSize.normalizedY.Value,
                false
            );
        }
    }
}
