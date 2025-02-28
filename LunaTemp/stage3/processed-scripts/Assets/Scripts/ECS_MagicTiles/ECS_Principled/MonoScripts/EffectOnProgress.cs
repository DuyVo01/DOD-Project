using Facade.Tweening;
using UnityEngine;

namespace ECS_MagicTile
{
    public class EffectOnProgress : MonoBehaviour
    {
        [SerializeField]
        private Vector2 startScale;

        [SerializeField]
        private Vector2 endScale;

        [SerializeField]
        private float duration;
        private RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Start()
        {
            // Sequence
            //     .Create(cycles: -1)
            //     .Chain(
            //         Tween.Scale(
            //             target: rectTransform,
            //             startValue: startScale,
            //             endValue: endScale,
            //             duration: duration,
            //             ease: Ease.Linear
            //         )
            //     )
            //     .Chain(
            //         Tween.Scale(
            //             target: rectTransform,
            //             endValue: startScale,
            //             duration: duration,
            //             ease: Ease.Linear
            //         )
            //     );

            Tweener
                .Sequence()
                .Chain(
                    Tweener
                        .DoScale(rectTransform, startScale, endScale, duration)
                        .SetEase(EaseType.Linear)
                )
                .Chain(
                    Tweener.DoScale(rectTransform, startScale, duration).SetEase(EaseType.Linear)
                )
                .SetLoops(-1);
        }
    }
}
