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
        return timeAppear * GlobalGameSetting.Instance.generalSetting.gameSpeed;
    }

    public static float CalculateScaleY(
        MusicNoteType noteType,
        float scaleX,
        float longNoteDuration = 1f
    )
    {
        float scaleY;
        if (noteType == MusicNoteType.ShortNote)
        {
            scaleY =
                scaleX
                + GlobalGameSetting.Instance.musicNoteSettingSO.shortNoteScaleYFactor * scaleX;
        }
        else
        {
            scaleY =
                (scaleX + longNoteDuration)
                * GlobalGameSetting.Instance.musicNoteSettingSO.longNoteScaleYFactor;
        }
        return scaleY;
    }
}
