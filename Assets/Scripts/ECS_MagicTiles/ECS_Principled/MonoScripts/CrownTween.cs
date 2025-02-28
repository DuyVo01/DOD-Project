using Facade.Tweening;
// using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class CrownTween : MonoBehaviour
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

            // Create a sequence for all tweens
            ISequence sequence = Tweener.Sequence();

            // First phase - all animations in parallel
            var firstPhaseScale = Tweener
                .DoScale(
                    target: currentCrown.crownRect,
                    startValue: currentCrown.scaleStartValue,
                    endValue: currentCrown.scaleMidValue,
                    duration: currentCrown.firstPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.firstPhaseEase));

            var firstPhaseImageScale = Tweener
                .DoScale(
                    target: currentCrown.crownAwakenedImg.rectTransform,
                    startValue: currentCrown.awakenedScaleStart,
                    endValue: currentCrown.awakenedScaleMid,
                    duration: currentCrown.firstPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.firstPhaseEase));

            var firstPhaseAlpha = Tweener
                .DoFade(
                    target: currentCrown.crownAwakenedImg,
                    startValue: currentCrown.alphaStart,
                    endValue: currentCrown.alphaMid,
                    duration: currentCrown.firstPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.firstPhaseEase));

            // Add all first phase animations to sequence
            sequence.Chain(firstPhaseScale);
            sequence.Join(firstPhaseImageScale);
            sequence.Join(firstPhaseAlpha);

            // Second phase - all animations in parallel
            var secondPhaseScale = Tweener
                .DoScale(
                    target: currentCrown.crownRect,
                    endValue: currentCrown.scaleEndValue,
                    duration: currentCrown.secondPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.secondPhaseEase));

            var secondPhaseImageScale = Tweener
                .DoScale(
                    target: currentCrown.crownAwakenedImg.rectTransform,
                    endValue: currentCrown.awakenedScaleEnd,
                    duration: currentCrown.secondPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.secondPhaseEase));

            var secondPhaseAlpha = Tweener
                .DoFade(
                    target: currentCrown.crownAwakenedImg,
                    endValue: currentCrown.alphaEnd,
                    duration: currentCrown.secondPhaseDuration
                )
                .SetEase(ConvertEase(currentCrown.secondPhaseEase));

            // Add all second phase animations to sequence
            sequence.Chain(secondPhaseScale);
            sequence.Join(secondPhaseImageScale);
            sequence.Join(secondPhaseAlpha);

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
            public int firstPhaseEase = 7; // Was Ease.OutCubic
            public float secondPhaseDuration = .2f;
            public int secondPhaseEase = 1; // Was Ease.Linear
        }
    }
}
