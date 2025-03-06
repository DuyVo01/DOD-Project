using ECS_MagicTile;
using UnityEngine;

public class PerfectLineCameraSpacePositionAdjuster : MonoBehaviour
{
    [Space(10)]
    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private PerfectLineSetting perfectLineSetting;

    [SerializeField]
    private Vector2 portraitNormalizedPos;

    [SerializeField]
    private Vector2 landscapeNormalizedPos;

    void Start()
    {
        this.enabled = false;
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
        // if (isPortrait)
        // {
        //     // Position object within camera view
        //     transform.position = CameraViewUtils.GetPositionInCameraView(
        //         targetCamera,
        //         portraitNormalizedPos.normalizedX.Value,
        //         portraitNormalizedPos.normalizedY.Value
        //     );
        // }
        // else
        // {
        //     // Position object within camera view
        //     transform.position = CameraViewUtils.GetPositionInCameraView(
        //         targetCamera,
        //         landscapeNormalizedPos.normalizedX.Value,
        //         landscapeNormalizedPos.normalizedY.Value
        //     );
        // }

        // perfectLineSetting.landscapeNormalizedPos = landscapeNormalizedPos;
        // perfectLineSetting.portraitNormalizedPos = portraitNormalizedPos;
    }
}
