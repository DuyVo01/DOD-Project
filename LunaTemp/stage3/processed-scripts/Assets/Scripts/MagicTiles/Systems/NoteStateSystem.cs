using UnityEngine;

public struct NoteStateSystem : IGameSystem
{
    public void NoteStateUpdate(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData,
        ref PerfectLineData perfectLineData
    )
    {
        float noteUpperY = musicNoteTransformData.TopLeft.Get(entityId).y;
        float noteLowerY = musicNoteTransformData.BottomLeft.Get(entityId).y;
        float perfectLineUpperY = perfectLineData.TopLeft.y;
        float perfectLineLowerY = perfectLineData.BottomLeft.y;

        if (musicNoteStateData.positionStates.Get(entityId) == MusicNotePositionState.OutOfScreen)
        {
            return;
        }

        if (noteLowerY < perfectLineUpperY && noteUpperY > perfectLineLowerY)
        {
            musicNoteStateData.positionStates.Set(
                entityId,
                MusicNotePositionState.InlineWithPerfectLine
            );
        }
        else if (noteUpperY < perfectLineLowerY)
        {
            musicNoteStateData.positionStates.Set(
                entityId,
                MusicNotePositionState.PassedPerfectLine
            );
        }
        else if (noteUpperY > perfectLineUpperY)
        {
            musicNoteStateData.positionStates.Set(
                entityId,
                MusicNotePositionState.AbovePerfectLine
            );
        }

        Vector2 noteTop = musicNoteTransformData.TopLeft.Get(entityId);
        if (
            CameraViewUtils.IsPositionOutOfBounds(
                Camera.main,
                noteTop,
                CameraViewUtils.CameraBoundCheck.Bottom
            )
        )
        {
            musicNoteStateData.positionStates.Set(entityId, MusicNotePositionState.OutOfScreen);
        }
    }

    public void NoteStateDeterminer(
        int entityId,
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        if (
            musicNoteMidiData
                .Durations[entityId]
                .IsInRange(musicNoteMidiData.MinDuration, musicNoteMidiData.MinDuration + 0.01f)
        )
        {
            musicNoteStateData.noteTypes.Set(entityId, MusicNoteType.ShortNote);
        }
        else if (musicNoteMidiData.Durations[entityId] > musicNoteMidiData.MinDuration)
        {
            musicNoteStateData.noteTypes.Set(entityId, MusicNoteType.LongNote);
        }
    }
}
