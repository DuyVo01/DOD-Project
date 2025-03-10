using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(fileName = "LaneLineSettings_SO", menuName = "Settings/LaneLineSettings")]
    public class LaneLineSettings : ScriptableObject
    {
        public GameObject landLinePrefab;
        public short laneLineCount = 5;
        public float laneLineWidth = .01f;
    }
}
