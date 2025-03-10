using UnityEngine;

namespace ECS_MagicTile.Components
{
    public struct TransformComponent : IComponent
    {
        public Vector2 PreviousPosition;
        public Vector2 Position;
        public Vector2 Size;
        public Vector2 rotation;
    }
}
