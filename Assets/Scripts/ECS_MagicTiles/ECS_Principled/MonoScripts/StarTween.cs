using System;
using Facade.Tweening;
// using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class StarTween : MonoBehaviour
    {
        // Helper method to convert PrimeTween.Ease values to our facade's EaseType
        private EaseType ConvertEase(int primeEase)
        {
            // Map the integer values from PrimeTween.Ease to our EaseType
            switch (primeEase)
            {
                case 1:
                    return EaseType.Linear;
                case 2:
                    return EaseType.InSine;
                case 3:
                    return EaseType.OutSine;
                case 4:
                    return EaseType.InOutSine;
                case 5:
                    return EaseType.InQuad;
                case 6:
                    return EaseType.OutQuad;
                case 7:
                    return EaseType.OutCubic;
                case 10:
                    return EaseType.InOutCubic;
                // Add more mappings as needed
                default:
                    return EaseType.Linear;
            }
        }

        [SerializeField]
        private StarProperties defaultValue;
        private StarProperties[] stars;
        private int currentStarIndexToProcess;

        StarProperties currentStar;

        private ISequence sequence;

        public void InitializeStars(StarProperties[] stars)
        {
            this.stars = stars;
            currentStarIndexToProcess = 0;
            Color color;
            for (int i = 0; i < stars.Length; i++)
            {
                color = stars[i].starAwakenedImg.color;
                color.a = 0;
                ;
                stars[i].starAwakenedImg.color = color;
            }
        }

        public void PlayEffect()
        {
            sequence?.Kill();
            sequence = Tweener.Sequence();

            currentStar = stars[currentStarIndexToProcess];
            Debug.Log($"Play Effect of {currentStar.starAwakenedImg.name}");

            sequence
                .Chain(
                    Tweener
                        .DoScale(
                            target: currentStar.starRect,
                            startValue: currentStar.scaleStartValue,
                            endValue: currentStar.scaleMidValue,
                            duration: currentStar.firstPhaseDuration
                        )
                        .SetEase(currentStar.firstPhaseEase)
                )
                .Join(
                    Tweener
                        .DoRotate(
                            target: currentStar.starRect,
                            startValue: currentStar.rotationStartValue,
                            endValue: currentStar.rotationMidValue,
                            duration: currentStar.firstPhaseDuration
                        )
                        .SetEase(currentStar.firstPhaseEase)
                )
                .Join(
                    Tweener
                        .DoFade(
                            target: currentStar.starAwakenedImg,
                            startValue: currentStar.alphaStart,
                            endValue: currentStar.alphaMid,
                            duration: currentStar.firstPhaseDuration
                        )
                        .SetEase(currentStar.firstPhaseEase)
                )
                .Join(
                    Tweener
                        .DoScale(
                            target: currentStar.starAwakenedImg.rectTransform,
                            startValue: currentStar.awakenedScaleStart,
                            endValue: currentStar.awakenedScaleMid,
                            duration: currentStar.firstPhaseDuration
                        )
                        .SetEase(currentStar.firstPhaseEase)
                )
                .Chain(
                    Tweener
                        .DoScale(
                            target: currentStar.starRect,
                            endValue: currentStar.scaleEndValue,
                            duration: currentStar.secondPhaseDuration
                        )
                        .SetEase(currentStar.secondPhaseEase)
                )
                .Join(
                    Tweener
                        .DoRotate(
                            target: currentStar.starRect,
                            endValue: currentStar.rotationEndValue,
                            duration: currentStar.secondPhaseDuration
                        )
                        .SetEase(currentStar.secondPhaseEase)
                )
                .Join(
                    Tweener
                        .DoFade(
                            target: currentStar.starAwakenedImg,
                            endValue: currentStar.alphaEnd,
                            duration: currentStar.secondPhaseDuration
                        )
                        .SetEase(currentStar.secondPhaseEase)
                )
                .Join(
                    Tweener
                        .DoScale(
                            target: currentStar.starAwakenedImg.rectTransform,
                            endValue: currentStar.awakenedScaleEnd,
                            duration: currentStar.secondPhaseDuration
                        )
                        .SetEase(currentStar.secondPhaseEase)
                );

            currentStarIndexToProcess++;
        }

        public bool IsAbleToPlay()
        {
            return currentStarIndexToProcess < stars.Length;
        }

        [Serializable]
        public class StarProperties
        {
            [Header("Star")]
            public RectTransform starRect;
            public Image starAwakenedImg;

            [Header("Scale properties")]
            public float scaleStartValue = 1f;
            public float scaleMidValue = 1.5f;
            public float scaleEndValue = 1f;

            [Header("Rotation properties")]
            public Vector3 rotationStartValue = new Vector3(0, 0, -180);
            public Vector3 rotationMidValue = new Vector3(0, 0, -240);
            public Vector3 rotationEndValue = new Vector3(0, 0, -360);

            [Header("Image awakened alpha properties")]
            public float awakenedScaleStart = 1f;
            public float awakenedScaleMid = 1.8f;
            public float awakenedScaleEnd = 1;
            public float alphaStart = 0;
            public float alphaMid = .5f;
            public float alphaEnd = 1f;

            [Header("Phase paremeters")]
            public float firstPhaseDuration = .4f;
            public EaseType firstPhaseEase = EaseType.OutCubic; // Was Ease.OutCubic
            public float secondPhaseDuration = .2f;
            public EaseType secondPhaseEase = EaseType.Linear; // Was Ease.Linear
        }
    }
}
