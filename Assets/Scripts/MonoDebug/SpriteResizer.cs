using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteResizer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera targetCamera;

    [Range(0, 1)]
    [SerializeField]
    private float widthPercentage = 0.5f;

    [Range(0, 1)]
    [SerializeField]
    private float heightPercentage = 0.5f;

    [SerializeField]
    private bool maintainAspectRatio = true;

    public void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Initialize();
            UpdateSize();
        }
    }

    private void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetCamera = Camera.main;
    }

    private void UpdateSize()
    {
        if (spriteRenderer == null || targetCamera == null)
            return;

        spriteRenderer.ResizeInCameraView(
            targetCamera,
            widthPercentage,
            heightPercentage,
            maintainAspectRatio
        );
    }
}
