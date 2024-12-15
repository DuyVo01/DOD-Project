using UnityEngine;

public struct PerfectLineData : IDataComponent
{
    public Vector2 TopLeft;
    public Vector2 TopRight;
    public Vector2 BottomLeft;
    public Vector2 BottomRight;

    public PerfectLineData(
        Vector2 topLeft,
        Vector2 topRight,
        Vector2 bottomLeft,
        Vector2 bottomRight
    )
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
    }
}
