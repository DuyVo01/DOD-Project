using EventChannel;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS_MagicTile
{
    public class GameStateManagerSystem : MonoBehaviour, IGameSystem
    {
        [Header("Event Channels")]
        [SerializeField]
        private EmptyEventChannel OnIntroGameoEventChannel;

        [SerializeField]
        private EmptyEventChannel OnInGameEventChannel;

        [SerializeField]
        private EmptyEventChannel OnOutroGameEventChannel;

        [Header("References")]
        [SerializeField]
        private GameObject introBlock;

        [SerializeField]
        private GameObject inGameBlock;

        [SerializeField]
        private GameObject outroBlock;

        [SerializeField]
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize()
        {
            OnIntroGameoEventChannel.Subscribe(OnIntro);
            OnInGameEventChannel.Subscribe(OnInGame);
            OnOutroGameEventChannel.Subscribe(OnOutro);
        }

        public void RunCleanup() { }

        public void RunUpdate(float deltaTime) { }

        private void OnIntro(EmptyData _)
        {
            introBlock.SetActive(true);
            inGameBlock.SetActive(false);
            outroBlock.SetActive(false);
        }

        private void OnInGame(EmptyData _)
        {
            introBlock.SetActive(false);
            inGameBlock.SetActive(true);
            outroBlock.SetActive(false);
        }

        private void OnOutro(EmptyData _)
        {
            introBlock.SetActive(false);
            inGameBlock.SetActive(false);
            outroBlock.SetActive(true);
        }
    }
}
