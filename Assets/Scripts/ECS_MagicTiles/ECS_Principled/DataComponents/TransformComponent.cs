using UnityEngine;

namespace ECS_MagicTile.Components
{
    public struct TransformComponent : IComponent
    {
        public Vector2 Posision;
        public Vector2 Size;
        public Vector2 rotation;
    }
}
