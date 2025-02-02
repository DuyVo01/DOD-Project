using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffectController : MonoBehaviour
{
    [System.Serializable]
    private struct SatelliteConfig
    {
        public float angleOffset; // Direction angle from center
        public float initialSpeed; // Initial burst speed
        public float driftSpeed; // Final slow drift speed
        public float speedDecayRate; // How quickly initial speed decays to drift
        public float scale; // Size relative to main image
    }

    [Header("Sprites")]
    [SerializeField]
    private Sprite perfectSprite;

    [SerializeField]
    private Sprite greatSprite;

    [Header("Main Image Settings")]
    [SerializeField]
    private Image mainImage;

    [SerializeField]
    private float mainScaleDuration = 0.2f;

    [SerializeField]
    private float holdDuration = 0.3f;

    [SerializeField]
    private float fadeOutDuration = 0.5f;

    [SerializeField]
    private float targetScale = 1.2f;

    [Header("Satellite Settings")]
    [SerializeField]
    private Image satellitePrefab;

    [SerializeField]
    private SatelliteConfig[] satelliteConfigs; // Configure each satellite's behavior

    [SerializeField]
    private float rotationMin = 0f;

    [SerializeField]
    private float rotationMax = 360f;

    private readonly List<Image> satelliteInstances = new();
    private Sequence currentAnimation;

    private void Awake()
    {
        // Create satellite instances at startup
        foreach (var _ in satelliteConfigs)
        {
            var satellite = Instantiate(satellitePrefab, transform);
            satellite.gameObject.SetActive(false);
            satelliteInstances.Add(satellite);
        }
    }

    public void PlayEffect(bool isPerfect)
    {
        // Kill any ongoing animation
        currentAnimation.Stop();

        // Reset state
        ResetEffectState(isPerfect);

        // Create new animation sequence
        currentAnimation = CreateEffectSequence();
    }

    private void ResetEffectState(bool isPerfect)
    {
        // Reset main image
        mainImage.sprite = isPerfect ? perfectSprite : greatSprite;
        mainImage.transform.localScale = Vector3.zero;
        mainImage.color = Color.white;

        // Reset and position satellites
        for (int i = 0; i < satelliteInstances.Count; i++)
        {
            var satellite = satelliteInstances[i];
            var config = satelliteConfigs[i];

            // Set size based on effect type
            float baseScale = isPerfect ? config.scale : config.scale * 0.1f;
            satellite.transform.localScale = Vector3.one * baseScale;

            // Reset position and set random rotation
            satellite.transform.localPosition = Vector3.zero;
            satellite.transform.rotation = Quaternion.Euler(
                0,
                0,
                Random.Range(rotationMin, rotationMax)
            );
            satellite.color = Color.white;
            satellite.gameObject.SetActive(true);
        }
    }

    private Sequence CreateEffectSequence()
    {
        var sequence = new Sequence();

        // Main image scale animation
        // PrimeTween uses a different approach for scale animations
        sequence.Group(
            Tween.Scale(
                mainImage.transform,
                targetScale * Vector3.one,
                mainScaleDuration,
                Ease.OutBack
            )
        );

        // Animate each satellite
        for (int i = 0; i < satelliteInstances.Count; i++)
        {
            var satellite = satelliteInstances[i];
            var config = satelliteConfigs[i];

            // Calculate movement direction
            var direction = Quaternion.Euler(0, 0, config.angleOffset) * Vector3.right;
            var targetPosition = direction * config.driftSpeed;

            // Create custom movement animation using our explosive motion
            sequence.Group(
                Tween.Custom(
                    0f,
                    1f,
                    fadeOutDuration,
                    onValueChange: progress =>
                    {
                        // Apply our custom easing for explosive movement
                        float easedProgress = CustomExplosiveEase(
                            progress,
                            config.initialSpeed,
                            config.speedDecayRate
                        );
                        satellite.transform.localPosition = Vector3.Lerp(
                            Vector3.zero,
                            targetPosition,
                            easedProgress
                        );
                    }
                )
            );
        }

        // Add hold time then fade everything
        sequence.Chain(Tween.Delay(holdDuration));

        // Fade main image
        sequence.Group(Tween.Alpha(mainImage, 0f, fadeOutDuration));

        // Fade satellites
        sequence.Group(
            Tween.Custom(
                1f,
                0f,
                fadeOutDuration,
                onValueChange: alpha =>
                {
                    foreach (var satellite in satelliteInstances)
                    {
                        satellite.color = new Color(1, 1, 1, alpha);
                    }
                }
            )
        );

        return sequence;
    }

    // Custom easing function for explosive movement
    private float CustomExplosiveEase(float progress, float initialSpeed, float decayRate)
    {
        // Creates an exponential decay curve that starts fast and settles to a drift
        return (initialSpeed * progress)
            - ((initialSpeed - 1) * (1 - Mathf.Exp(-decayRate * progress)));
    }
}
