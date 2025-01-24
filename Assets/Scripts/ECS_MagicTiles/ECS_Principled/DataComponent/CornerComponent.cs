using ECS_MagicTile;
using UnityEngine;

public struct CornerComponent : IComponent
{
    public Vector2 TopLeft;
    public Vector2 TopRight;
    public Vector2 BottomLeft;
    public Vector2 BottomRight;
}
