using ECS_MagicTile;
using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(fileName = "GeneralGameSetting_SO", menuName = "Settings/GeneralGameSetting")]
    public class GeneralGameSetting : ScriptableObject
    {
        public float GameSpeed;
        public EGameState startState;

        public float PreciseGameSpeed { get; set; }
        public EGameState CurrentGameState { get; set; } = EGameState.IngamePrestart;
    }
}
