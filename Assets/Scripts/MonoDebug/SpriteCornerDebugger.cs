#if UNITY_EDITOR
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteCornerDebugger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color gizmoColor = Color.yellow;

    [SerializeField]
    private float gizmoSize = 0.2f;

    [SerializeField]
    PerfectLineSettingSO generalGameSettingSO;

    private void OnDrawGizmos()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return;

        // Cache sprite properties
        Vector2 position = transform.position;
        Vector2 spriteSize = Vector2.Scale(spriteRenderer.sprite.bounds.size, transform.localScale);

        // Calculate half sizes
        float halfWidth = spriteSize.x / 2f;
        float halfHeight = spriteSize.y / 2f;

        // Calculate corners
        Vector2 topLeft = position + new Vector2(-halfWidth, halfHeight);
        Vector2 topRight = position + new Vector2(halfWidth, halfHeight);
        Vector2 bottomLeft = position + new Vector2(-halfWidth, -halfHeight);
        Vector2 bottomRight = position + new Vector2(halfWidth, -halfHeight);

        // Draw corners
        Gizmos.color = gizmoColor;
        DrawCorner(topLeft);
        DrawCorner(topRight);
        DrawCorner(bottomLeft);
        DrawCorner(bottomRight);

        generalGameSettingSO.TopLeft = topLeft;
        generalGameSettingSO.TopRight = topRight;
        generalGameSettingSO.BottomLeft = bottomLeft;
        generalGameSettingSO.BottomRight = bottomRight;
        generalGameSettingSO.Position = transform.position;
    }

    private void DrawCorner(Vector2 position)
    {
        Gizmos.DrawWireSphere(position, gizmoSize);
    }

    // Optional: Add this if you want to see the full bounds
    private void OnDrawGizmosSelected()
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return;

        // Draw sprite bounds
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.3f);
        Vector2 spriteSize = Vector2.Scale(spriteRenderer.sprite.bounds.size, transform.localScale);
        Gizmos.DrawWireCube(transform.position, spriteSize);
    }
}
#endif
