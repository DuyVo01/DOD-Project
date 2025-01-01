using ECS_MagicTile;

public struct MusicNoteComponent : IComponent
{
    public int PostionId;
    public float TimeAppear;
    public float Duration;
    public MusicNoteType musicNoteType;
}
