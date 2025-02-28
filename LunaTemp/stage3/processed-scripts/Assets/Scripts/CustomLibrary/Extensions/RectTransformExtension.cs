using UnityEngine;

public static class RectTransformExtension
{
    public static void SetAnchorPosition(this RectTransform rectTransform, Vector2 anchorPosition)
    {
        rectTransform.anchoredPosition = anchorPosition;
    }

    public static void SetSizeDelta(this RectTransform rectTransform, Vector2 sizeDelta)
    {
        rectTransform.sizeDelta = sizeDelta;
    }

    public static void SetLocalScale(this Transform rectTransform, Vector3 localScale)
    {
        rectTransform.localScale = localScale;
    }

    public static void SetRotationEuler(this Transform rectTransform, Vector3 rotationEuler)
    {
        rectTransform.rotation = Quaternion.Euler(rotationEuler);
    }
}
