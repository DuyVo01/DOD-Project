using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField]
    private Sprite[] frames;

    [SerializeField]
    private float fps = 12f;

    [SerializeField]
    private bool loop = true;

    private Image image;
    private float timer;
    private int currentFrame;
    private bool isPlaying = true;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isPlaying || frames == null || frames.Length <= 0)
            return;

        // Update timer and frame
        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame++;

            // Handle looping
            if (currentFrame >= frames.Length)
            {
                if (loop)
                    currentFrame = 0;
                else
                {
                    isPlaying = false;
                    return;
                }
            }

            // Update sprite
            image.sprite = frames[currentFrame];
        }
    }

    public void Play()
    {
        isPlaying = true;
        currentFrame = 0;
        timer = 0f;
    }

    public void Stop()
    {
        isPlaying = false;
    }
}
