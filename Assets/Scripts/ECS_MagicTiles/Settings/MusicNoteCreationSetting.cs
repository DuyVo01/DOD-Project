using UnityEngine;

[CreateAssetMenu(
    fileName = "MusicNoteCreationSetting_SO",
    menuName = "Settings/MusicNoteCreationSetting"
)]
public class MusicNoteCreationSetting : ScriptableObject
{
    [Header("Song Data")]
    public TextAsset MidiContent;

    [Header("Note Configuration")]
    public float ShortNoteScaleYFactor;
    public float LongNoteScaleYFactor;

    [Header("Prefabs")]
    public GameObject LongTilePrefab;
    public GameObject ShortTilePrefab;
}
