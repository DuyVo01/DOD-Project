using ComponentCache;
using EventChannel;
using Facade.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GameStateManagerSystem : MonoBehaviour, IGameSystem
    {
        [Header("Event Channels")]
        [SerializeField]
        private EmptyEventChannel OnIntroGameEventChannel;

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
        private GameObject transition;

        [SerializeField]
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        ISequence sequence;

        void Start() { }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize()
        {
            OnIntroGameEventChannel.Subscribe(OnIntro);
            OnInGameEventChannel.Subscribe(OnInGame);
            OnOutroGameEventChannel.Subscribe(OnOutro);
        }

        public void RunCleanup() { }

        public void RunUpdate(float deltaTime) { }

        private void OnIntro(EmptyData _)
        {
            if (transition.Image() == null)
            {
                transition.RegisterComponent(transition.GetComponent<Image>());
            }

            sequence?.Kill();

            sequence ??= Tweener
                .Sequence()
                .Chain(
                    Tweener
                        .DoFade(transition.Image(), 1f, 1f)
                        .OnComplete(() =>
                        {
                            introBlock.SetActive(true);
                            inGameBlock.SetActive(false);
                            outroBlock.SetActive(false);
                        })
                )
                .Chain(Tweener.DoFade(transition.Image(), 0f, 1f));
        }

        private void OnInGame(EmptyData _)
        {
            if (transition.Image() == null)
            {
                transition.RegisterComponent(transition.GetComponent<Image>());
            }

            sequence?.Kill();

            sequence ??= Tweener
                .Sequence()
                .Chain(
                    Tweener
                        .DoFade(transition.Image(), 1f, 1f)
                        .OnComplete(() =>
                        {
                            introBlock.SetActive(false);
                            inGameBlock.SetActive(true);
                            outroBlock.SetActive(false);
                        })
                )
                .Chain(Tweener.DoFade(transition.Image(), 0f, 1f));
        }

        private void OnOutro(EmptyData _)
        {
            introBlock.SetActive(false);
            inGameBlock.SetActive(false);
            outroBlock.SetActive(true);
        }
    }
}
