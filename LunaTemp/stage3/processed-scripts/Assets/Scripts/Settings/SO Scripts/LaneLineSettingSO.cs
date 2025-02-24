using UnityEngine;

[CreateAssetMenu(fileName = "LaneLineSettingSO", menuName = "Setting/LaneLineSetting")]
public class LaneLineSettingSO : ScriptableObject
{
    [Header("Visual Settings")]
    [Range(0.001f, 0.1f)]
    [Tooltip("Width of lane lines as percentage of screen width")]
    public float lineWidthPercentage = 0.01f;

    [Header("Color Settings")]
    public Color lineColor = Color.white;
}
