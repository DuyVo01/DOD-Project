using System;
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
    public struct SpriteCorners
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 Center;
        public Vector2 Size;
    }

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

    public enum PivotPoint2D
    {
        Top,
        Bottom,
        Left,
        Right,
    }

    /// <summary>
    /// Scales a sprite from a specified pivot point while maintaining the pivot position.
    /// </summary>
    /// <param name="spriteRenderer">The sprite renderer to scale</param>
    /// <param name="newScale">The target scale (x,y)</param>
    /// <param name="pivot">The pivot point to scale from</param>
    /// /// <returns>True if scaling succeeded, false otherwise</returns>
    public static bool ScaleFromPivot(
        SpriteRenderer spriteRenderer,
        Vector2 newScale,
        PivotPoint2D pivot
    )
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return false;

        var transform = spriteRenderer.transform;

        Vector3 originalPosition = transform.position;
        Vector2 originalScale = transform.localScale;

        // Get sprite size in world space
        Vector2 worldSize = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);

        // Calculate size after scaling
        Vector2 newWorldSize = new Vector2(
            worldSize.x * (newScale.x / originalScale.x),
            worldSize.y * (newScale.y / originalScale.y)
        );

        // Calculate offset in world space
        Vector2 offset = CalculateOffset(pivot, worldSize, newWorldSize);

        // Apply changes
        transform.localScale = new Vector3(newScale.x, newScale.y, transform.localScale.z);
        transform.position = originalPosition + (Vector3)offset;

        return true;
    }

    private static Vector2 CalculateOffset(
        PivotPoint2D pivot,
        Vector2 originalSize,
        Vector2 newSize
    )
    {
        // Calculate the actual change in size
        Vector2 sizeDiff = newSize - originalSize;

        return pivot switch
        {
            PivotPoint2D.Top => new Vector2(0, -sizeDiff.y / 2),
            PivotPoint2D.Bottom => new Vector2(0, sizeDiff.y / 2),
            PivotPoint2D.Left => new Vector2(-sizeDiff.x / 2, 0),
            PivotPoint2D.Right => new Vector2(sizeDiff.x / 2, 0),
            _ => Vector2.zero,
        };
    }
}
