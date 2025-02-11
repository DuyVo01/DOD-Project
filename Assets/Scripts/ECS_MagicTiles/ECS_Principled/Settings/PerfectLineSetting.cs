using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(
        fileName = "PerfectLineSetting_SO",
        menuName = "Settings/PerfectLineSetting_SO"
    )]
    public class PerfectLineSetting : ScriptableObject
    {
        [Header(" Normalize Positions")]
        public PositionPreset portraitNormalizedPos;

        public PositionPreset landscapeNormalizedPos;

        [System.Serializable]
        public struct PositionPreset
        {
            [Range(0, 1)]
            public float normalizedX;

            [Range(0, 1)]
            public float normalizedY;
        }
    }
}
