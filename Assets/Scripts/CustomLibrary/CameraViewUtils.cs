using UnityEngine;

public static class CameraViewUtils
{
    /// <summary>
    /// Gets the camera view boundaries in world units
    /// </summary>
    public static Rect GetCameraViewBounds(Camera camera)
    {
        if (!camera.orthographic)
        {
            Debug.LogWarning("GetCameraViewBounds is designed for orthographic cameras");
        }

        float height = camera.orthographicSize * 2f;
        float width = height * camera.aspect;

        // Calculate boundaries based on camera position
        float leftBound = camera.transform.position.x - width / 2f;
        float rightBound = camera.transform.position.x + width / 2f;
        float bottomBound = camera.transform.position.y - height / 2f;
        float topBound = camera.transform.position.y + height / 2f;

        return new Rect(leftBound, bottomBound, width, height);
    }

    /// <summary>
    /// Gets position within camera view based on normalized coordinates (0-1)
    /// </summary>
    public static Vector3 GetPositionInCameraView(
        Camera camera,
        float normalizedX,
        float normalizedY,
        float z = 0f
    )
    {
        Rect bounds = GetCameraViewBounds(camera);

        float x = Mathf.Lerp(bounds.xMin, bounds.xMax, normalizedX);
        float y = Mathf.Lerp(bounds.yMin, bounds.yMax, normalizedY);

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Calculates scale needed to make sprite fill specified portion of camera view
    /// </summary>
    public static Vector2 CalculateScaleInCameraView(
        Camera camera,
        Sprite sprite,
        float widthPercentage,
        float heightPercentage,
        bool maintainAspectRatio = true
    )
    {
        if (sprite == null)
            return Vector2.one;

        // Get sprite's original size in world units
        Vector2 spriteSize = sprite.bounds.size;

        // Calculate camera view size in world units
        float cameraHeight = camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * camera.aspect;

        // Calculate target size in world units
        float targetWidth = cameraWidth * widthPercentage;
        float targetHeight = cameraHeight * heightPercentage;

        // Calculate required scale
        Vector2 scale = new Vector2(targetWidth / spriteSize.x, targetHeight / spriteSize.y);

        if (maintainAspectRatio)
        {
            float minScale = Mathf.Min(scale.x, scale.y);
            scale.x = minScale;
            scale.y = minScale;
        }

        return scale;
    }

    public enum CameraBoundCheck
    {
        Top,
        Bottom,
        Right,
        Left,
        All,
    }

    /// <summary>
    /// Checks if a position is out of camera bounds for the specified check type
    /// </summary>
    /// <param name="camera">The camera to check bounds against</param>
    /// <param name="position">The world position to check</param>
    /// <param name="boundCheck">The type of bound check to perform</param>
    /// <param name="padding">Optional padding to add to the bounds (can be negative to shrink bounds)</param>
    /// <returns>True if the position is out of bounds for the specified check</returns>
    public static bool IsPositionOutOfBounds(
        Camera camera,
        Vector3 position,
        CameraBoundCheck boundCheck,
        float padding = 0f
    )
    {
        Rect bounds = GetCameraViewBounds(camera);

        // Apply padding
        bounds.xMin += padding;
        bounds.xMax -= padding;
        bounds.yMin += padding;
        bounds.yMax -= padding;

        return boundCheck switch
        {
            CameraBoundCheck.Top => position.y > bounds.yMax,
            CameraBoundCheck.Bottom => position.y < bounds.yMin,
            CameraBoundCheck.Left => position.x < bounds.xMin,
            CameraBoundCheck.Right => position.x > bounds.xMax,
            CameraBoundCheck.All => !bounds.Contains(new Vector2(position.x, position.y)),
            _ => false,
        };
    }
}
