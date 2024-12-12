using System;
using UnityEngine;

public struct TileSpawnSystem
{
    public void SpawnTile(int[] posIDs, float[] timeAppears, ref Vector2[] notePositions)
    {
        float posX;
        for (int i = 0; i < posIDs.Length; i++)
        {
            posX = MagicTileHelper.GetXPositionBasedOnPosID(posIDs[i]);
            notePositions[i].x = posX;
        }
        float posY;
        for (int i = 0; i < timeAppears.Length; i++)
        {
            posY = MagicTileHelper.GetYPositionBasedOnTimeAppear(timeAppears[i]);
            notePositions[i].y = posY;
        }
    }
}
