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

    public static float CalculateScaleY(
        float scaleX,
        float scaleFactor,
        float longNoteDuration = 0f // this mean the note is short note
    )
    {
        float scaleY;

        scaleY = (scaleX + longNoteDuration) * scaleFactor;

        return scaleY;
    }
}
