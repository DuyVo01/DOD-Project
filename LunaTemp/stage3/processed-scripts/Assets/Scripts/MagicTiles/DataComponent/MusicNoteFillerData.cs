using UnityEngine;

public struct MusicNoteFillerData : IDataComponent
{
    public ChunkArray<float> FillPercent;
    public ChunkArray<bool> IsVisibles;

    public MusicNoteFillerData(int capacity)
    {
        FillPercent = new ChunkArray<float>(capacity);
        IsVisibles = new ChunkArray<bool>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            FillPercent.Add(0f);
            IsVisibles.Add(false);
        }
    }
}
