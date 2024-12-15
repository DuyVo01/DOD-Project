using UnityEngine;

public static class SpriteExtension
{
    /// <summary>
    /// Resize sprite to fill percentage of camera view
    /// </summary>
    public static void ResizeInCameraView(
        this SpriteRenderer spriteRenderer,
        Camera camera,
        float widthPercentage,
        float heightPercentage,
        bool maintainAspectRatio = true
    )
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return;

        Vector2 scale = CameraViewUtils.CalculateScaleInCameraView(
            camera,
            spriteRenderer.sprite,
            widthPercentage,
            heightPercentage,
            maintainAspectRatio
        );

        spriteRenderer.transform.localScale = new Vector3(scale.x, scale.y, 1f);
    }
}

public static class SpriteUtility
{
    public static SpriteCorners GetSpriteCorners(SpriteRenderer spriteRenderer)
    {
        Vector2 position = spriteRenderer.transform.position;
        Vector2 spriteSize = Vector2.Scale(
            spriteRenderer.sprite.bounds.size,
            spriteRenderer.transform.localScale
        );

        float halfWidth = spriteSize.x / 2f;
        float halfHeight = spriteSize.y / 2f;

        return new SpriteCorners
        {
            TopLeft = position + new Vector2(-halfWidth, halfHeight),
            TopRight = position + new Vector2(halfWidth, halfHeight),
            BottomLeft = position + new Vector2(-halfWidth, -halfHeight),
            BottomRight = position + new Vector2(halfWidth, -halfHeight),
            Center = position,
            Size = spriteSize,
        };
    }

    public struct SpriteCorners
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 Center;
        public Vector2 Size;
    }
}
