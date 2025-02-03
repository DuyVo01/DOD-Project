using EventChannel;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class ScoreEffectController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField]
        private BoolEventChannel scoreSignalEffectChannel;

        [Header("Main Image")]
        [SerializeField]
        private RectTransform mainImageRect;

        [Header("Satellite Images")]
        [SerializeField]
        private SatelliteImages[] satelliteImages;

        private Sequence sequence;
        private Image mainImage;

        void Awake()
        {
            mainImage = mainImageRect.GetComponent<Image>();
        }

        void OnEnable()
        {
            scoreSignalEffectChannel.Subscribe(PlayEffect);
            sequence = Sequence.Create();
        }

        void OnDisable()
        {
            scoreSignalEffectChannel.Unsubscribe(PlayEffect);
        }

        private void PlayEffect(bool isPerfect)
        {
            sequence.Stop();

            Color color = mainImage.color;
            color.a = 1;
            mainImage.color = color;

            sequence = Tween
                .Scale(
                    target: mainImageRect,
                    startValue: Vector3.zero,
                    endValue: Vector3.one,
                    duration: 0.2f,
                    ease: Ease.OutSine
                )
                .Chain(Tween.Delay(duration: .5f))
                .Chain(Tween.Alpha(target: mainImage, startValue: 1, endValue: 0, duration: 3));
        }

        [System.Serializable]
        public class SatelliteImages
        {
            public RectTransform satelliteRect;
            public Vector2 startAnchoredPos;
            public float moveSpeed;
            public float rotation;
        }
    }
}
