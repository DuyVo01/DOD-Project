using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct MusicNoteFillerData : IDataComponent
{
    public ChunkArray<Vector2> Positions;
    public ChunkArray<Vector2> Sizes;
    public ChunkArray<bool> IsVisibles;

    public MusicNoteFillerData(int capacity)
    {
        Positions = new ChunkArray<Vector2>(capacity);
        Sizes = new ChunkArray<Vector2>(capacity);
        IsVisibles = new ChunkArray<bool>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            Positions.Add(Vector2.zero);
            Sizes.Add(Vector2.zero);
            IsVisibles.Add(false);
        }
    }
}
