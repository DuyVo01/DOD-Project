using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicNoteTransformData : IDataComponent
{
    public ChunkArray<int> entityIDs;
    public ChunkArray<Vector3> positions;
    public int count;

    public MusicNoteTransformData(int capacity)
    {
        entityIDs = new ChunkArray<int>(capacity);
        positions = new ChunkArray<Vector3>(capacity);
        count = 0;
    }
}
