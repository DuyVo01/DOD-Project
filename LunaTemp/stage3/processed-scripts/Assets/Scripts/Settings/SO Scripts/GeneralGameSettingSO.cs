using UnityEngine;

[CreateAssetMenu(fileName = "GeneralGameSettings", menuName = "Setting/General Game Settings")]
public class GeneralGameSettingSO : ScriptableObject
{
    [Header("General")]
    public int gameSpeed;
    public float baseScaleYForNote = 1;
    public TextAsset midiContent;
}
