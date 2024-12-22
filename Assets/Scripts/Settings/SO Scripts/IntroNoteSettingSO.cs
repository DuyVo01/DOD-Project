using UnityEngine;

[CreateAssetMenu(fileName = "LaneLineSettingSO", menuName = "Setting/LaneLineSetting")]
public class IntroNoteSettingSO : ScriptableObject
{
    [Header("Note Configuration")]
    public float introNoteScaleYFactor = 1.1f;
    public int initLane = 2;
}
