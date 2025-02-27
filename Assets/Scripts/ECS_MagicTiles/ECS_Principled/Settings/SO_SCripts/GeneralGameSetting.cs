using ECS_MagicTile;
using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(fileName = "GeneralGameSetting_SO", menuName = "Settings/GeneralGameSetting")]
    public class GeneralGameSetting : ScriptableObject
    {
        public float GameSpeed;

        public float PreciseGameSpeed { get; set; }
        public EGameState CurrentGameState { get; set; } = EGameState.IngamePrestart;
    }
}
