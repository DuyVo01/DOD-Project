using UnityEngine;

public class CameraSpacePositionAdjuster : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    [Range(0, 1)]
    public float normalizedX = 0.5f;

    [Range(0, 1)]
    public float normalizedY = 0.5f;

    public void OnValidate()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;
        if (targetCamera != null)
        {
            // Position object within camera view
            transform.position = CameraViewUtils.GetPositionInCameraView(
                targetCamera,
                normalizedX,
                normalizedY
            );
        }
    }
}
