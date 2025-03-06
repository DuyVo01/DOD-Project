using ComponentCache;
using EventChannel;
using Facade.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class GameStateManagerSystem : MonoBehaviour, IGameSystem
    {
        [Header("Data SO")]
        [SerializeField]
        private GeneralGameSetting generalGameSetting;

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

        int[] eventSubscriptions = new int[3];

        void Start() { }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunInitialize()
        {
            eventSubscriptions[0] = OnIntroGameEventChannel.Subscribe(
                target: this,
                (target, data) => OnIntro(data)
            );
            eventSubscriptions[1] = OnInGameEventChannel.Subscribe(
                target: this,
                (target, data) => OnInGame(data)
            );
            eventSubscriptions[2] = OnOutroGameEventChannel.Subscribe(
                target: this,
                (target, data) => OnOutro(data)
            );

            if (generalGameSetting.CurrentGameState == EGameState.Intro)
            {
                OnIntro(EmptyData.Default());
            }
            else if (generalGameSetting.CurrentGameState == EGameState.IngamePrestart)
            {
                OnInGame(EmptyData.Default());
            }
            else if (generalGameSetting.CurrentGameState == EGameState.Outro)
            {
                OnOutro(EmptyData.Default());
            }
        }

        public void RunCleanup()
        {
            OnIntroGameEventChannel.Unsubscribe(eventSubscriptions[0]);
            OnInGameEventChannel.Unsubscribe(eventSubscriptions[1]);
            OnOutroGameEventChannel.Unsubscribe(eventSubscriptions[2]);
        }

        public void RunUpdate(float deltaTime) { }

        private void OnIntro(EmptyData _)
        {
            if (transition.Image() == null)
            {
                transition.RegisterComponent(transition.GetComponent<Image>());
            }

            sequence?.Kill();

            sequence = Tweener
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

            sequence = Tweener
                .Sequence()
                .Chain(
                    Tweener
                        .DoFade(transition.Image(), 1f, 1f)
                        .OnComplete(() =>
                        {
                            generalGameSetting.CurrentGameState = EGameState.IngamePrestart;

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
