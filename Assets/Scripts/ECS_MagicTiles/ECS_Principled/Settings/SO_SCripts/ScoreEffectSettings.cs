using UnityEngine;

namespace ECS_MagicTile.Settings
{
    [CreateAssetMenu(
        fileName = "ScoreEffectSettings_SO",
        menuName = "Settings/ScoreEffectSettings"
    )]
    public class ScoreEffectSettings : ScriptableObject
    {
        [Header("Perfect Score Effect")]
        public GameObject perfectScoreEffectPrefab;

        [Header("Great Score Prefab")]
        public GameObject greatScoreEffectPrefab;
    }
}
