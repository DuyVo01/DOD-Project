using UnityEngine;

public static class MagicTileHelper
{
    public static float GetXPositionBasedOnPosID(int posId)
    {
        return posId * 10f;
    }

    public static float GetYPositionBasedOnTimeAppear(float timeAppear)
    {
        return timeAppear * GlobalGameSetting.Instance.generalSetting.gameSpeed;
    }

    public static float ConvertDurationToAppropiateScaleY(
        float smallestDuration,
        float baseScaleY,
        float targetDuration
    )
    {
        return baseScaleY * targetDuration / smallestDuration;
    }
}

public static class SpawnPositionCalculator
{
    /// <summary>
    /// Calculates spawn position based on posID and perfect line corners
    /// </summary>
    public static float CalculateSpawnX(int posID, Vector2 topLeft, Vector2 topRight)
    {
        // Guard against invalid posID
        if (posID < 0 || posID > 3)
            return 0f;

        float lineWidth = topRight.x - topLeft.x;
        float segmentWidth = lineWidth / 3f; // 3 segments for 4 positions

        // Calculate position
        return topLeft.x + (posID * segmentWidth);
    }
}
