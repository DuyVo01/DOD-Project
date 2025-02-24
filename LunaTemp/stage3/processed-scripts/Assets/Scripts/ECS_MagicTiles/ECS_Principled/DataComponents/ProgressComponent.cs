using UnityEngine;

namespace ECS_MagicTile
{
    public struct ProgressComponent : IComponent
    {
        public float MaxProgressRawValue;
        public float CurrentProgressRawValue;
        public float currentProgressPercent;
    }
}
