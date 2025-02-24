using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(fileName = "LaneLineSettings_SO", menuName = "Settings/LaneLineSettings")]
    public class LaneLineSettings : ScriptableObject
    {
        public GameObject landLinePrefab;

        public RangeReactiveFloat laneLineWidth;

        private void OnValidate()
        {
            laneLineWidth.OnChangeValidatedInInpsector();
        }
    }
}
