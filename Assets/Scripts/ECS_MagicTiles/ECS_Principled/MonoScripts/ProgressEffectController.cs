using EventChannel;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    [RequireComponent(typeof(StarTween))]
    [RequireComponent(typeof(CrownTween))]
    public class ProgressEffectController : MonoBehaviour
    {
        [SerializeField]
        private Slider progressSlider;

        [SerializeField]
        private RectTransform[] progressPoints;

        [SerializeField]
        private StarTween.StarProperties[] starPoints;

        [SerializeField]
        private CrownTween.CrownProperties[] crownPoints;

        private RectTransform progressSliderRect;

        private float[] segmentValues;

        private int currentPassedSegmentPoint;

        private StarTween starTween;
        private CrownTween crownTween;

        void Awake()
        {
            starTween = GetComponent<StarTween>();
            starTween.InitializeStars(starPoints);

            crownTween = GetComponent<CrownTween>();
            crownTween.InitializeCrowns(crownPoints);
        }

        void OnEnable()
        {
            progressSlider.onValueChanged.AddListener(OnProgressValueChanged);
        }

        void OnDisable()
        {
            progressSlider.onValueChanged.RemoveListener(OnProgressValueChanged);
        }

        void Start()
        {
            SetupPoints();
        }

        void OnValidate()
        {
            SetupPoints();
        }

        private void SetupPoints()
        {
            if (progressPoints.Length == 0 || progressSlider == null)
            {
                return;
            }

            if (progressSliderRect == null)
            {
                progressSliderRect = progressSlider.GetComponent<RectTransform>();
            }

            int pointCount = progressPoints.Length;
            float sliderWidth = progressSliderRect.sizeDelta.x;
            float segmentWidth = sliderWidth / pointCount;
            float baseSegmentValue = 1f / pointCount;

            segmentValues = new float[pointCount];

            for (int i = 0; i < progressPoints.Length; i++)
            {
                if (progressPoints[i] == null)
                    continue;
                progressPoints[i].anchoredPosition = new Vector2(segmentWidth * (i + 1), 0);

                segmentValues[i] = (i + 1) * baseSegmentValue;
            }

            currentPassedSegmentPoint = 1;
        }

        private void OnProgressValueChanged(float value)
        {
            int index = currentPassedSegmentPoint - 1;

            if (value >= segmentValues[index])
            {
                if (starTween.IsAbleToPlay())
                {
                    starTween.PlayEffect();
                }
                else if (crownTween.IsAbleToPlay())
                {
                    crownTween.PlayEffect();
                }

                currentPassedSegmentPoint++;
            }
        }
    }
}
