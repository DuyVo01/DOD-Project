using ECS_MagicTile;

namespace ECS_MagicTile.Components
{
    public struct MusicNoteComponent : IComponent
    {
        public int PostionId;
        public float TimeAppear;
        public float Duration;
        public MusicNoteType musicNoteType;
    }
}
