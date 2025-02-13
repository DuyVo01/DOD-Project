using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class StarTween : MonoBehaviour
    {
        [SerializeField]
        private StarProperties defaultValue;
        private StarProperties[] stars;
        private int currentStarIndexToProcess;

        StarProperties currentStar;

        public void InitializeStars(StarProperties[] stars)
        {
            this.stars = stars;
            currentStarIndexToProcess = 0;
        }

        public void PlayEffect()
        {
            currentStar = stars[currentStarIndexToProcess];

            Tween
                .Scale(
                    target: currentStar.starRect,
                    startValue: currentStar.scaleStartValue,
                    endValue: currentStar.scaleMidValue,
                    duration: currentStar.firstPhaseDuration,
                    ease: currentStar.firstPhaseEase
                )
                .Group(
                    Tween.Rotation(
                        target: currentStar.starRect,
                        startValue: currentStar.rotationStartValue,
                        endValue: currentStar.rotationMidValue,
                        duration: currentStar.firstPhaseDuration,
                        ease: currentStar.firstPhaseEase
                    )
                )
                .Group(
                    Tween.Alpha(
                        target: currentStar.starAwakenedImg,
                        startValue: currentStar.alphaStart,
                        endValue: currentStar.alphaMid,
                        duration: currentStar.firstPhaseDuration,
                        ease: currentStar.firstPhaseEase
                    )
                )
                .Group(
                    Tween.Scale(
                        target: currentStar.starAwakenedImg.rectTransform,
                        startValue: currentStar.awakenedScaleStart,
                        endValue: currentStar.awakenedScaleMid,
                        duration: currentStar.firstPhaseDuration,
                        ease: currentStar.firstPhaseEase
                    )
                )
                .Chain(
                    Tween
                        .Scale(
                            target: currentStar.starRect,
                            startValue: currentStar.scaleMidValue,
                            endValue: currentStar.scaleEndValue,
                            duration: currentStar.secondPhaseDuration,
                            ease: currentStar.secondPhaseEase
                        )
                        .Group(
                            Tween.Rotation(
                                target: currentStar.starRect,
                                startValue: currentStar.rotationMidValue,
                                endValue: currentStar.rotationEndValue,
                                duration: currentStar.secondPhaseDuration,
                                ease: currentStar.secondPhaseEase
                            )
                        )
                        .Group(
                            Tween.Alpha(
                                target: currentStar.starAwakenedImg,
                                startValue: currentStar.alphaMid,
                                endValue: currentStar.alphaEnd,
                                duration: currentStar.secondPhaseDuration,
                                ease: currentStar.secondPhaseEase
                            )
                        )
                        .Group(
                            Tween.Scale(
                                target: currentStar.starAwakenedImg.rectTransform,
                                startValue: currentStar.awakenedScaleMid,
                                endValue: currentStar.awakenedScaleEnd,
                                duration: currentStar.secondPhaseDuration,
                                ease: currentStar.secondPhaseEase
                            )
                        )
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
            public Ease firstPhaseEase = Ease.OutCubic;
            public float secondPhaseDuration = .2f;
            public Ease secondPhaseEase = Ease.Linear;
        }
    }
}
