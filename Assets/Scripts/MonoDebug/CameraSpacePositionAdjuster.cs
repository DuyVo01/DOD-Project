using EventChannel;
using UnityEngine;

public class CameraSpacePositionAdjuster : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField]
    private BoolEventChannel OnOrientationChangedChannel;

    [Space(10)]
    [SerializeField]
    private Camera targetCamera;

    [Header(" Normalize Positions")]
    [SerializeField]
    private PositionPreset portraitNormalizedPos;

    [SerializeField]
    private PositionPreset landscapeNormalizePos;

    void OnEnable()
    {
        OnOrientationChangedChannel.Subscribe(OnOrientationChanged);
    }

    void OnDisable()
    {
        OnOrientationChangedChannel.Unsubscribe(OnOrientationChanged);
    }

    public void OnValidate()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera == null)
        {
            return;
        }

        if (Screen.currentResolution.height > Screen.currentResolution.width)
        {
            OnOrientationChanged(true);
        }
        else
        {
            OnOrientationChanged(false);
        }
    }

    private void OnOrientationChanged(bool isPortrait)
    {
        if (isPortrait)
        {
            // Position object within camera view
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                portraitNormalizedPos.normalizedX,
                portraitNormalizedPos.normalizedY
            );
        }
        else
        {
            // Position object within camera view
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                landscapeNormalizePos.normalizedX,
                landscapeNormalizePos.normalizedY
            );
        }
    }

    [System.Serializable]
    private struct PositionPreset
    {
        [Range(0, 1)]
        public float normalizedX;

        [Range(0, 1)]
        public float normalizedY;
    }
}
