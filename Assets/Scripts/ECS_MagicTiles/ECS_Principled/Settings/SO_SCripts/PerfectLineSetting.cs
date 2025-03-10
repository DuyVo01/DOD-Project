using System;
using Unity.VisualScripting;
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

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Width { get; private set; }

        public void UpdateWidth(SpriteRenderer spriteRenderer)
        {
            SpriteUtility.SpriteCorners spriteCorners = SpriteUtility.GetSpriteCorners(
                spriteRenderer
            );
            Width = spriteCorners.TopRight.x - spriteCorners.TopLeft.x;
        }
    }
}
