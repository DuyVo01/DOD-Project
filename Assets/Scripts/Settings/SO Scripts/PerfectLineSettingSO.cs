using UnityEngine;

[CreateAssetMenu(fileName = "PerfectLineSetting", menuName = "Setting/Perfect Line Settings")]
public class PerfectLineSettingSO : ScriptableObject
{
    [Header("Perfect Line")]
    public Vector2 TopLeft;
    public Vector2 TopRight;
    public Vector2 BottomLeft;
    public Vector2 BottomRight;

    public float PerfectLineWidth()
    {
        return TopRight.x - TopLeft.x;
    }
}
