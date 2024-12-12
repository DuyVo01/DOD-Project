using UnityEngine;

public struct MovingTileSystem
{
    public void MovingTile(ref MusicNoteMidiData musicNoteMidiData)
    {
        Vector2 position;
        for (int i = 0; i < musicNoteMidiData.Positions.Length; i++)
        {
            position = musicNoteMidiData.Positions[i];
            position.y -=
                musicNoteMidiData.TimeAppears[i]
                * GlobalGameSetting.Instance.generalSetting.gameSpeed
                * Time.deltaTime;

            musicNoteMidiData.Positions[i] = position;
        }
    }
}
