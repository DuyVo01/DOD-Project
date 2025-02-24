using UnityEngine;

[CreateAssetMenu(fileName = "IntroNoteSettingSO", menuName = "Setting/IntroNoteSetting")]
public class IntroNoteSettingSO : ScriptableObject
{
    [Header("Note Configuration")]
    public float introNoteScaleYFactor = 1.1f;
    public int initLane = 2;
}
