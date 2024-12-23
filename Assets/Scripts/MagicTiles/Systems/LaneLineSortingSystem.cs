using UnityEngine;

public struct LaneLineSortingSystem : IGameSystem
{
    // Pseudo setting - replace with actual ScriptableObject reference later

    public void PositionLandLine(
        int entityId,
        ref LaneLineData landLineData,
        ref PerfectLineData perfectLineData
    )
    {
        // Calculate total width from perfect line bounds
        float totalWidth = perfectLineData.TopRight.x - perfectLineData.TopLeft.x;

        // Calculate lane width (total width / 4 for 4 lanes)
        float laneWidth = totalWidth / 4f;

        // Calculate x position based on entity ID
        float lineX;
        if (entityId == 0)
        {
            // First line - left edge
            lineX = perfectLineData.TopLeft.x;
        }
        else if (entityId == 4)
        {
            // Last line - right edge
            lineX = perfectLineData.TopRight.x;
        }
        else
        {
            // Internal dividing lines (entityId 1,2,3)
            // Position after each lane (1/4, 2/4, 3/4 of total width)
            lineX = perfectLineData.TopLeft.x + (laneWidth * (entityId));
        }

        // Calculate normalized x position (0-1 range)
        float normalizedX = (lineX - perfectLineData.TopLeft.x) / totalWidth;

        // Position the line
        Vector2 worldPosition = CameraViewUtils.GetPositionInCameraView(
            Camera.main,
            normalizedX,
            0.5f,
            0f
        );

        // Set position
        landLineData.Positions.Set(entityId, worldPosition);

        // Calculate and set size
        float lineWidthPercentage = GlobalGameSetting.Instance.laneLineSettings.lineWidthPercentage;
        Vector2 size = CalculateLineSize(Camera.main, lineWidthPercentage);
        landLineData.Sizes.Set(entityId, size);

#if UNITY_EDITOR
        Debug.Log(
            $"Lane line {entityId} positioned at x: {worldPosition.x}, normalized: {normalizedX}, isEdge: {entityId == 0 || entityId == 4}"
        );
#endif
    }

    private Vector2 CalculateLineSize(Camera camera, float widthPercentage)
    {
        // Magic number 3 to ensure that the line always fill entire screen,
        // as camera view does not cover entire screen height on some devices
        float cameraHeight = camera.orthographicSize * 2f + 3;
        float cameraWidth = cameraHeight * camera.aspect;

        return new Vector2(cameraWidth * widthPercentage, cameraHeight);
    }
}
