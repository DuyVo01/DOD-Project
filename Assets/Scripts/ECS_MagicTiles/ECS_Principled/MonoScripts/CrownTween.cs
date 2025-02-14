using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class CrownTween : MonoBehaviour
    {
        [SerializeField]
        private CrownProperties defaultValue;
        private CrownProperties[] crowns;
        private CrownProperties currentCrown;
        private int currentCrownIndexToProcess;

        public void InitializeCrowns(CrownProperties[] crownProperties)
        {
            this.crowns = crownProperties;
            currentCrownIndexToProcess = 0;
            Color color;

            for (int i = 0; i < crowns.Length; i++)
            {
                color = crowns[i].crownAwakenedImg.color;
                color.a = 0;
                ;
                crowns[i].crownAwakenedImg.color = color;
            }
        }

        public void PlayEffect()
        {
            currentCrown = crowns[currentCrownIndexToProcess];

            Tween
                .Scale(
                    target: currentCrown.crownRect,
                    startValue: currentCrown.scaleStartValue,
                    endValue: currentCrown.scaleMidValue,
                    duration: currentCrown.firstPhaseDuration,
                    ease: currentCrown.firstPhaseEase
                )
                .Group(
                    Tween.Scale(
                        target: currentCrown.crownAwakenedImg.rectTransform,
                        startValue: currentCrown.awakenedScaleStart,
                        endValue: currentCrown.awakenedScaleMid,
                        duration: currentCrown.firstPhaseDuration,
                        ease: currentCrown.firstPhaseEase
                    )
                )
                .Group(
                    Tween.Alpha(
                        target: currentCrown.crownAwakenedImg,
                        startValue: currentCrown.alphaStart,
                        endValue: currentCrown.alphaMid,
                        duration: currentCrown.firstPhaseDuration,
                        ease: currentCrown.firstPhaseEase
                    )
                )
                .Chain(
                    Tween
                        .Scale(
                            target: currentCrown.crownRect,
                            startValue: currentCrown.scaleMidValue,
                            endValue: currentCrown.scaleEndValue,
                            duration: currentCrown.secondPhaseDuration,
                            ease: currentCrown.secondPhaseEase
                        )
                        .Group(
                            Tween.Scale(
                                target: currentCrown.crownAwakenedImg.rectTransform,
                                startValue: currentCrown.awakenedScaleMid,
                                endValue: currentCrown.awakenedScaleEnd,
                                duration: currentCrown.secondPhaseDuration,
                                ease: currentCrown.secondPhaseEase
                            )
                        )
                        .Group(
                            Tween.Alpha(
                                target: currentCrown.crownAwakenedImg,
                                startValue: currentCrown.alphaMid,
                                endValue: currentCrown.alphaEnd,
                                duration: currentCrown.secondPhaseDuration,
                                ease: currentCrown.secondPhaseEase
                            )
                        )
                );
            currentCrownIndexToProcess++;
        }

        public bool IsAbleToPlay()
        {
            return currentCrownIndexToProcess < crowns.Length;
        }

        [System.Serializable]
        public class CrownProperties
        {
            [Header("Crown")]
            public RectTransform crownRect;
            public Image crownAwakenedImg;

            [Header("Scale properties")]
            public float scaleStartValue = 1f;
            public float scaleMidValue = 1.5f;
            public float scaleEndValue = 1f;

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
