using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(
        fileName = "MusicNoteCreationSetting_SO",
        menuName = "Settings/MusicNoteCreationSetting"
    )]
    public class MusicNoteCreationSetting : ScriptableObject
    {
        [Header("Note Calculation Mode")]
        public bool UsePreciseNoteCalculation;

        [Header("Song Data")]
        public TextAsset MidiContent;

        [Header("Note Configuration")]
        [Header("Portrait")]
        public float ShortNoteScaleYFactor;
        public float LongNoteScaleYFactor;
        public float startingNoteLane;

        [Header("Prefabs")]
        public GameObject LongTilePrefab;
        public GameObject ShortTilePrefab;
        public GameObject startingNotePrefab;
    }
}
