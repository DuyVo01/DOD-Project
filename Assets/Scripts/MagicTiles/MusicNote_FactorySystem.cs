using UnityEngine;

public class MusicNote_FactorySystem
{
    private ChunkArray<MusicNote_DataComponent> musicNotes;

    public MusicNote_FactorySystem()
    {
        musicNotes = new ChunkArray<MusicNote_DataComponent>(1);
    }

    public ChunkArray<MusicNote_DataComponent> MusicNotes => musicNotes;
}
