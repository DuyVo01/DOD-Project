using ECS_MagicTile;
using ECS_MagicTile.Components;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class ScoreUISyncer : ArchetypeSyncer
    {
        public override EGameState GameStateToExecute => EGameState.All;

        protected override Archetype Archetype => Archetype.Registry.GameScore;

        private readonly Text scoreText;

        // Instead of creating UI, we receive references to existing UI elements
        public ScoreUISyncer(GlobalPoint globalPoint)
        {
            this.scoreText = globalPoint.scoreText;
        }

        public override void RunUpdate(float deltaTime)
        {
            // Get score data from storage
            ScoreComponent scoreComponent = DedicatedStorage.GetComponents<ScoreComponent>()[0];

            // Update UI
            scoreText.text = $"Score: {scoreComponent.TotalScore}";
        }
    }
}
