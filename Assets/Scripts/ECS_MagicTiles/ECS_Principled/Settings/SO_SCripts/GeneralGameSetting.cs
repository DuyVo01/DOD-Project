using UnityEngine;

[CreateAssetMenu(fileName = "GeneralGameSetting_SO", menuName = "Settings/GeneralGameSetting")]
public class GeneralGameSetting : ScriptableObject
{
    public float GameSpeed;

    public float PreciseGameSpeed { get; set; }
}
