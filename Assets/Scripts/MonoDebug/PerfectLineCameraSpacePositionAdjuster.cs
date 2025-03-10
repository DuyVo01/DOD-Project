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

#if UNITY_EDITOR
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
#endif

    private void OnOrientationChanged(bool isPortrait)
    {
        if (isPortrait)
        {
            // Position object within camera view
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                portraitNormalizedPos.x,
                portraitNormalizedPos.y
            );
        }
        else
        {
            // Position object within camera view
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                landscapeNormalizedPos.x,
                landscapeNormalizedPos.y
            );
        }

        perfectLineSetting.landscapeNormalizedPos = landscapeNormalizedPos;
        perfectLineSetting.portraitNormalizedPos = portraitNormalizedPos;
        perfectLineSetting.Position = transform.position;
        perfectLineSetting.Size = transform.localScale;
    }
}
