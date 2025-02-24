using ECS_MagicTile.Components;
using UnityEngine.UI;

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
        private readonly Text scoreText;

        public void SyncGameScore([Bridge.Ref] ScoreComponent scoreComponent)
        {
            scoreText.text = $"Score: {scoreComponent.TotalScore}";
        }
    }
}
