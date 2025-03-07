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
        public Vector2 portraitNormalizedPos;

        public Vector2 landscapeNormalizedPos;

        [Header("Normalized Size")]
        public Vector2 portraitNormalizedSize;

        public Vector2 landscapeNormalizedSize;

        private void OnValidate() { }
    }
}
