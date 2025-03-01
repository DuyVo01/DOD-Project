using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GameIntroSystem : MonoBehaviour, IGameSystem
    {
        [Header("Data")]
        [SerializeField]
        private GeneralGameSetting generalGameSetting;

        [Header("Component references")]
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private GameObject introBlock;

        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public void RunCleanup()
        {
            startButton.onClick.RemoveAllListeners();
        }

        public void RunInitialize()
        {
            startButton.onClick.AddListener(() =>
            {
                generalGameSetting.CurrentGameState = EGameState.IngamePrestart;
                introBlock.SetActive(false);
            });
        }

        public void RunUpdate(float deltaTime) { }

        public void SetWorld(World world)
        {
            World = world;
        }
    }
}
