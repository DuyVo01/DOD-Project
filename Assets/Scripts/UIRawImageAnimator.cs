using UnityEngine;
using UnityEngine.UI;

public class UIRawImageAnimator : MonoBehaviour
{
    [Header("Sprite Sheet Settings")]
    [SerializeField]
    private int columns = 4;

    [SerializeField]
    private int rows = 4;

    [SerializeField]
    private float fps = 12f;

    [SerializeField]
    private bool loop = true;

    private RawImage rawImage;
    private float timer;
    private int currentFrame;
    private int totalFrames;
    private float frameWidth;
    private float frameHeight;
    private bool isPlaying = true;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        totalFrames = columns * rows;
        frameWidth = 1f / columns;
        frameHeight = 1f / rows;

        // Initialize to first frame
        UpdateUVRect(0);
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame++;

            if (currentFrame >= totalFrames)
            {
                if (loop)
                    currentFrame = 0;
                else
                {
                    isPlaying = false;
                    return;
                }
            }

            UpdateUVRect(currentFrame);
        }
    }

    private void UpdateUVRect(int frameIndex)
    {
        int row = frameIndex / columns;
        int col = frameIndex % columns;

        // Calculate UV coordinates
        float x = col * frameWidth;
        float y = 1f - (row + 1) * frameHeight; // Unity UV starts from bottom-left

        rawImage.uvRect = new Rect(x, y, frameWidth, frameHeight);
    }

    public void Play()
    {
        isPlaying = true;
        currentFrame = 0;
        timer = 0f;
        UpdateUVRect(0);
    }

    public void Stop()
    {
        isPlaying = false;
    }
}
