using System;
using UnityEngine;

public struct TileSpawnSystem : ITileSpawnSystem, IGameSystem
{
    public void SpawnTileNote(ref MusicNoteMidiData musicNoteMidiData)
    {
        int[] posIDs = musicNoteMidiData.PositionIds;
        float[] timeAppears = musicNoteMidiData.TimeAppears;

        float posX;
        for (int i = 0; i < posIDs.Length; i++)
        {
            posX = MagicTileHelper.GetXPositionBasedOnPosID(posIDs[i]);
            musicNoteMidiData.SetPositionX(i, posX);
        }
        float posY;
        for (int i = 0; i < timeAppears.Length; i++)
        {
            posY = MagicTileHelper.GetYPositionBasedOnTimeAppear(timeAppears[i]);
            musicNoteMidiData.SetPositionY(i, posY);
        }
    }
}
