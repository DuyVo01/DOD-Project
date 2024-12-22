using UnityEngine;

public struct IntroNoteInitSystem : IGameSystem
{
    public void PrepareIntroNote(
        ref IntroNoteData introNoteData,
        ref PerfectLineData perfectLineData
    )
    {
        float totalWidth = perfectLineData.TopRight.x - perfectLineData.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

        int laneToSpawn = GlobalGameSetting.Instance.introNoteSetting.initLane;

        // Calculate final position
        float spawnX = perfectLineData.TopLeft.x + (laneToSpawn * laneWidth) + halfLaneWidth;

        float spawnY = perfectLineData.Position.y;

        // Set both MIDI data and transform position in one go
        introNoteData.Position = new Vector2(spawnX, spawnY);
    }
}
