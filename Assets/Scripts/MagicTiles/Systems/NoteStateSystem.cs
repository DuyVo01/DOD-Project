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
        float noteUpperY;
        float noteLowerY;
        float perfectLineUpperY = perfectLineData.TopLeft.y;
        float perfectLineLowerY = perfectLineData.BottomLeft.y;

        if (
            musicNoteStateData.positionStates.Get(entityId)
            == MusicNotePositionState.PassedPerfectLine
        )
        {
            return;
        }
        noteUpperY = musicNoteTransformData.TopLeft.Get(entityId).y;
        noteLowerY = musicNoteTransformData.BottomLeft.Get(entityId).y;

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

            //Debug.Log($"Entity {entityId} has passed perfect line");
        }
    }

    public void NoteStateDeterminer(
        int entityId,
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        if (musicNoteMidiData.Durations[entityId] == musicNoteMidiData.MinDuration)
        {
            musicNoteStateData.noteTypes.Set(entityId, MusicNoteType.ShortNote);
        }
        else if (musicNoteMidiData.Durations[entityId] > musicNoteMidiData.MinDuration)
        {
            musicNoteStateData.noteTypes.Set(entityId, MusicNoteType.LongNote);
        }
    }
}
