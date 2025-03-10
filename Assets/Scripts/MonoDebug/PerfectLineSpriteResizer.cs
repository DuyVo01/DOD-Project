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
    private Vector2 portraitNormalizedSize;

    [SerializeField]
    private Vector2 landscapeNormalizedSize;

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

#if UNITY_EDITOR
    private void UpdateSize(bool isPortrait)
    {
        if (spriteRenderer == null || targetCamera == null)
            return;

        if (isPortrait)
        {
            spriteRenderer.ResizeInCameraView(
                targetCamera,
                portraitNormalizedSize.x,
                portraitNormalizedSize.y,
                maintainAspectRatio
            );
        }
        else
        {
            spriteRenderer.ResizeInCameraView(
                targetCamera,
                landscapeNormalizedSize.x,
                landscapeNormalizedSize.y,
                maintainAspectRatio
            );
        }

        perfectLineSetting.portraitNormalizedSize = portraitNormalizedSize;
        perfectLineSetting.landscapeNormalizedSize = landscapeNormalizedSize;
        perfectLineSetting.Size = transform.localScale;
        perfectLineSetting.Position = transform.position;
        perfectLineSetting.UpdateWidth(spriteRenderer);
    }
#endif
}
