using UnityEngine;

public struct TransformUpdateSystem : ITransformUpdateSystem, IGameSystem
{
    public void SyncTransform(
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteTransformData musicNoteTransformData
    )
    {
        int entityId = -1;
        Vector3 transformPos = Vector3.zero;
        for (int i = 0; i < musicNoteTransformData.count; i++)
        {
            entityId = musicNoteTransformData.entityIDs.Get(i);
            transformPos.Set(musicNoteMidiData.PosX[entityId], musicNoteMidiData.PosY[entityId], 0);

            musicNoteTransformData.positions.Set(entityId, transformPos);
        }
    }
}
