using System.Collections.Generic;
using EventChannel;
using Facade.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AspectFillBackgroundTweaker : MonoBehaviour
{
    [SerializeField]
    private BoolEventChannel onOrientationChanged;

    [Header("Settings")]
    [SerializeField]
    private bool useAspectFill = true;

    [SerializeField]
    private bool animateTransition = true;

    [SerializeField]
    private float transitionDuration = 0.3f;

    [SerializeField]
    List<BackgroundElement> backgroundElements = new List<BackgroundElement>();

    void Start()
    {
        if (!useAspectFill)
        {
            return;
        }
        OnOrientationChanged(Screen.width < Screen.height);

        onOrientationChanged.Subscribe(this, (target, data) => OnOrientationChanged(data));
    }

    void OnDestroy()
    {
        onOrientationChanged.Subscribe(this, (target, data) => OnOrientationChanged(data));
        
    }

    private void OnOrientationChanged(bool isPortrait)
    {
        for (int i = 0; i < backgroundElements.Count; i++)
        {
            if (isPortrait)
            {
                backgroundElements[i].backgroundImage.sprite = backgroundElements[i].portraitSprite;
            }
            else
            {
                backgroundElements[i].backgroundImage.sprite = backgroundElements[
                    i
                ].landscapeSprite;
            }
            backgroundElements[i].originalHeight = backgroundElements[i]
                .backgroundImage
                .sprite
                .rect
                .height;
            backgroundElements[i].originalWidth = backgroundElements[i]
                .backgroundImage
                .sprite
                .rect
                .width;
            ApplyAspectFill(backgroundElements[i]);
        }
    }

    public void RegisterBackground(BackgroundElement element)
    {
        for (int i = 0; i < backgroundElements.Count; i++)
        {
            if (element == backgroundElements[i])
            {
                Debug.Log("Element already registered");
                return;
            }
        }
        element.originalHeight = element.backgroundImage.sprite.rect.height;
        element.originalWidth = element.backgroundImage.sprite.rect.width;

        backgroundElements.Add(element);
    }

    public void UnregisterBackground(BackgroundElement element)
    {
        backgroundElements.Remove(element);
    }

    private void ApplyAspectFill(BackgroundElement element)
    {
        if (element.backgroundImage.sprite.IsNull())
        {
            return;
        }

        //Get current screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate aspect ratio
        float screenAspect = screenWidth / screenHeight;
        float imageAspect = element.originalWidth / element.originalHeight;

        float scaleFactor;
        Vector2 sizeDelta = Vector2.zero;
        if (screenAspect > imageAspect)
        {
            scaleFactor = screenWidth / element.originalWidth;
            float scaleHeight = element.originalHeight * scaleFactor;
            float heighDifferent = Mathf.Abs(screenHeight - scaleHeight);
            sizeDelta = new Vector2(0, heighDifferent);
        }
        else
        {
            // Screen is taller than image, match height and exceed width
            scaleFactor = screenHeight / element.originalHeight;
            float scaledWidth = element.originalWidth * scaleFactor;
            float widthDifference = scaledWidth - screenWidth;

            // Apply the width difference to sizeDelta to grow beyond screen bounds
            sizeDelta = new Vector2(widthDifference, 0);
        }

        Tweener
            .TweenValue(
                element.backgroundRect.sizeDelta,
                sizeDelta,
                animateTransition ? transitionDuration : 0,
                element.backgroundRect.SetSizeDelta
            )
            .SetEase(EaseType.OutQuad);
    }

    [System.Serializable]
    public class BackgroundElement
    {
        public Sprite portraitSprite;
        public Sprite landscapeSprite;
        public RectTransform backgroundRect;
        public Image backgroundImage;
        public float originalWidth;
        public float originalHeight;
    }
}
