using UnityEngine;

public struct MusicNoteStateData : IDataComponent
{
    public ChunkArray<MusicNoteType> noteTypes;
    public ChunkArray<MusicNotePositionState> positionStates;
    public ChunkArray<MusicNoteInteractiveState> interactiveStates;
    public ChunkArray<float> filler;

    public MusicNoteStateData(int capacity)
    {
        noteTypes = new ChunkArray<MusicNoteType>(capacity);
        positionStates = new ChunkArray<MusicNotePositionState>(capacity);
        interactiveStates = new ChunkArray<MusicNoteInteractiveState>(capacity);
        filler = new ChunkArray<float>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            noteTypes.Add(MusicNoteType.ShortNote);
            positionStates.Add(MusicNotePositionState.AbovePerfectLine);
            interactiveStates.Add(MusicNoteInteractiveState.Normal);
            filler.Add(0f);
        }
    }
}

public enum MusicNoteType
{
    ShortNote,
    LongNote,
}

public enum MusicNotePositionState
{
    AbovePerfectLine,
    InlineWithPerfectLine,
    PassedPerfectLine,
    OutOfScreen,
}

public enum MusicNoteInteractiveState
{
    Normal,
    Pressed,
    Hold,
    Completed,
}
