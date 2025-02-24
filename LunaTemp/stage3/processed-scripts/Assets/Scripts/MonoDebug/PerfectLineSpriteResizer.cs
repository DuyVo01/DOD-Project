using ECS_MagicTile;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PerfectLineSpriteResizer : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private PerfectLineSetting perfectLineSetting;

    [SerializeField]
    private PerfectLineSetting.NormalizedFloatPreset portraitNormalizedSize;

    [SerializeField]
    private PerfectLineSetting.NormalizedFloatPreset landscapeNormalizedSize;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool maintainAspectRatio = true;

    public void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Initialize();

            UpdateSize(Screen.currentResolution.height > Screen.currentResolution.width);
        }
    }

    private void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetCamera = Camera.main;
    }

    private void UpdateSize(bool isPortrait)
    {
        // if (spriteRenderer == null || targetCamera == null)
        //     return;

        // if (isPortrait)
        // {
        //     spriteRenderer.ResizeInCameraView(
        //         targetCamera,
        //         portraitNormalizedSize.normalizedX.Value,
        //         portraitNormalizedSize.normalizedY.Value,
        //         maintainAspectRatio
        //     );
        // }
        // else
        // {
        //     spriteRenderer.ResizeInCameraView(
        //         targetCamera,
        //         landscapeNormalizedSize.normalizedX.Value,
        //         landscapeNormalizedSize.normalizedY.Value,
        //         maintainAspectRatio
        //     );
        // }

        // perfectLineSetting.portraitNormalizedSize = portraitNormalizedSize;
        // perfectLineSetting.landscapeNormalizedSize = landscapeNormalizedSize;
    }
}
