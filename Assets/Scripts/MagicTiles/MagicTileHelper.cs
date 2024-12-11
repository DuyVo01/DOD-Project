using System;
using UnityEngine;

public static class MagicTileHelper
{
    public static float GetXPositionBasedOnPosID(int posId)
    {
        return posId * 10f;
    }

    public static float GetYPositionBasedOnTimeAppear(float timeAppear)
    {
        return timeAppear;
    }
}
