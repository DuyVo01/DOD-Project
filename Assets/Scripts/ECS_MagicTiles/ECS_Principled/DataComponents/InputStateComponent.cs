using UnityEngine;

namespace ECS_MagicTile.Components
{
    public struct InputStateComponent : IComponent
    {
        public bool IsActive;
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public InputState State;
        public int FrameCount;
    }
}
