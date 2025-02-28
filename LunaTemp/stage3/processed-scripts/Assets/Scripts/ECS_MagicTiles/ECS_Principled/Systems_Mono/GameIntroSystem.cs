using ComponentCache;
using ECS_MagicTile;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GameIntroSystem : MonoBehaviour, IGameSystem
    {
        [Header("Data")]
        [SerializeField]
        private GeneralGameSetting generalGameSetting;

        [SerializeField]
        private Button startButton;

        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public EGameState GameStateToExecute => EGameState.All;

        public void RunCleanup()
        {
            startButton.onClick.RemoveAllListeners();
        }

        public void RunInitialize()
        {
            startButton.onClick.AddListener(() =>
            {
                generalGameSetting.CurrentGameState = EGameState.IngamePrestart;
            });
        }

        public void RunUpdate(float deltaTime) { }

        public void SetWorld(World world)
        {
            World = world;
        }
    }
}
