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

    public enum PivotPointXY
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
    /// <returns>True if scaling succeeded, false otherwise</returns>
    public static bool ScaleFromPivot(
        SpriteRenderer spriteRenderer,
[Bridge.Ref]         Vector2 newScale,
        PivotPointXY pivot
    )
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return false;

        var transform = spriteRenderer.transform;
        Vector3 originalPosition = transform.position;

        // Get current pivot point position in world space
        Vector3 pivotWorldPosition = GetPivotWorldPosition(spriteRenderer, pivot);

        // Apply new scale
        transform.localScale = new Vector3(newScale.x, newScale.y, transform.localScale.z);

        // Get new pivot point position after scaling
        Vector3 newPivotWorldPosition = GetPivotWorldPosition(spriteRenderer, pivot);

        // Calculate and apply position correction
        Vector3 correction = pivotWorldPosition - newPivotWorldPosition;
        transform.position = originalPosition + correction;

        return true;
    }

    private static Vector3 GetPivotWorldPosition(SpriteRenderer spriteRenderer, PivotPointXY pivot)
    {
        Bounds bounds = spriteRenderer.bounds;
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        Vector3 result;

        switch (pivot)
        {
            case PivotPointXY.Top:
                result = center + new Vector3(0, extents.y, 0);
                break;
            case PivotPointXY.Bottom:
                result = center - new Vector3(0, extents.y, 0);
                break;
            case PivotPointXY.Left:
                result = center - new Vector3(extents.x, 0, 0);
                break;
            case PivotPointXY.Right:
                result = center + new Vector3(extents.x, 0, 0);
                break;
            default:
                result = center;
                break;
        }

        return result;
    }

    /// <summary>
    /// Resize sprite to fill percentage of camera view
    /// </summary>
    public static Vector3 ResizeInCameraView(
        SpriteRenderer spriteRenderer,
        Camera camera,
        float widthPercentage,
        float heightPercentage,
        bool maintainAspectRatio = true
    )
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null)
            return Vector3.zero;

        Vector2 scale = CameraViewUtils.CalculateScaleInCameraView(
            camera,
            spriteRenderer.sprite,
            widthPercentage,
            heightPercentage,
            maintainAspectRatio
        );

        return new Vector3(scale.x, scale.y, 1f);
    }
}
