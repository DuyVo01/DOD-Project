using System.Collections;
using UnityEngine;

public static class AudioSourceExtension
{
    public static void PlayWithFadeIn(
        this AudioSource source,
        MonoBehaviour monoSource,
        float fadeDuration = 1f,
        float maxVolume = 1f
    )
    {
        source.volume = 0f; // Start with zero volume
        source.Play(); // Start playing

        // Start the fade coroutine
        monoSource.StartCoroutine(FadeIn(source, fadeDuration, maxVolume));
    }

    private static IEnumerator FadeIn(AudioSource source, float duration, float maxVolume)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            // Calculate how far we are through the fade
            float progress = (Time.time - startTime) / duration;

            // Use smoothstep for natural-feeling fade
            source.volume = Mathf.SmoothStep(0f, maxVolume, progress);

            Debug.Log("source Volume:" + source.volume);

            yield return null;
        }

        // Ensure we end at exactly full volume
        source.volume = 1f;
    }
}
