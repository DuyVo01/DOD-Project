using UnityEngine;

public struct TransformUpdateSystem
{
    public void SyncTransform(
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteTransformData musicNoteTransformData
    )
    {
        int entityId = -1;

        for (int i = 0; i < musicNoteTransformData.count; i++)
        {
            entityId = musicNoteTransformData.entityIDs.Get(i);

            musicNoteTransformData.positions.Set(i, musicNoteMidiData.Positions[entityId]);
        }
    }
}
