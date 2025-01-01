using UnityEngine;

[CreateAssetMenu(
    fileName = "MusicNoteCreationSettings_SO",
    menuName = "System settings/MusicNoteCreationSettings"
)]
public class MusicNoteCreationSettings : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject longTilePrefab;
    public GameObject shortTilePrefab;

    //*---------------------------------------

    [Header("Note Configuration")]
    public float shortNoteScaleYFactor = 1.1f;
    public float longNoteScaleYFactor = 1.5f;
}
