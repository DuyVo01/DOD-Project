using UnityEngine;

public struct IntroNoteInitSystem : IGameSystem
{
    public void PrepareIntroNote(
        ref IntroNoteData introNoteData,
        ref PerfectLineData perfectLineData
    )
    {
        // Calculate lane width
        float totalWidth = perfectLineData.TopRight.x - perfectLineData.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

        int laneToSpawn = GlobalGameSetting.Instance.introNoteSetting.initLane;

        // Calculate X position (centered in lane)
        float spawnX = perfectLineData.TopLeft.x + (laneToSpawn * laneWidth) + halfLaneWidth;

        // Calculate Y position (centered on perfect line)
        float spawnY = (perfectLineData.TopLeft.y + perfectLineData.BottomLeft.y) / 2f;

        // Set position
        introNoteData.Position = new Vector2(spawnX, spawnY);

        Debug.Log($"Intro Note Position - X: {spawnX}, Y: {spawnY}");
        Debug.Log(
            $"Perfect Line - Top: {perfectLineData.TopLeft.y}, Bottom: {perfectLineData.BottomLeft.y}"
        );
    }
}
