using UnityEngine;

[CreateAssetMenu(fileName = "MusicNoteSetting", menuName = "Setting/MusicNoteSetting")]
public class MusicNoteSettingSO : ScriptableObject
{
    [Header("Note Configuration")]
    public float shortNoteScaleYFactor = 1.1f;
    public float longNoteScaleYFactor = 1.5f;
}
