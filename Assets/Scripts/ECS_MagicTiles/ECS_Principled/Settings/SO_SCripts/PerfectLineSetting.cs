using System;
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
        [Range(0, 1)]
        public Vector2 portraitNormalizedPos;

        public Vector2 landscapeNormalizedPos;

        [Header("Normalized Size")]
        [Range(0, 1)]
        public Vector2 portraitNormalizedSize;

        public Vector2 landscapeNormalizedSize;

        private void OnValidate() { }
    }
}
