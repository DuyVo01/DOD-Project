using ECS_MagicTile.Components;
using TMPro;
using UnityEngine;

namespace ECS_MagicTile
{
    public class GameScoreSyncTool : BaseSyncTool
    {
        public GameScoreSyncTool(GlobalPoint globalPoint)
            : base(globalPoint)
        {
            scoreText = globalPoint.scoreText;
        }

        protected override Archetype Archetype => Archetype.Registry.GameScore;
        private readonly TextMeshProUGUI scoreText;

        public void SyncGameScore(ScoreComponent scoreComponent)
        {
            scoreText.text = $"Score: {scoreComponent.TotalScore}";
        }
    }
}
