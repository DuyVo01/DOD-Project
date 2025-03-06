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

    private int[] eventSubscriptionIds = new int[2];

    void Start()
    {
        perfectLineVisualSprite = GetComponent<SpriteRenderer>();
        UpdatePerfectLineFakeVisualSize(0);
    }

    void OnEnable()
    {
        eventSubscriptionIds[0] = onOrientationChangedChannel.Subscribe(
            this,
            (target, data) => UpdatePosition(data)
        );
        eventSubscriptionIds[1] = onOrientationChangedChannel.Subscribe(
            this,
            (target, data) => UpdateSize(data)
        );
    }

    void OnDisable()
    {
        onOrientationChangedChannel.Unsubscribe(eventSubscriptionIds[0]);
        onOrientationChangedChannel.Unsubscribe(eventSubscriptionIds[1]);
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
                perfectLineSetting.portraitNormalizedPos.x,
                perfectLineSetting.portraitNormalizedPos.y
            );
        }
        else
        {
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                perfectLineSetting.landscapeNormalizedPos.x,
                perfectLineSetting.landscapeNormalizedPos.y
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
                perfectLineSetting.portraitNormalizedSize.y,
                false
            );
        }
        else
        {
            perfectLineVisualSprite.ResizeInCameraView(
                targetCamera,
                1,
                perfectLineSetting.landscapeNormalizedSize.y,
                false
            );
        }
    }
}
