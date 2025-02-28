using UnityEngine;
using UnityEngine.UI;

namespace Facade.Tweening
{
    public static class Tweener
    {
        // Fade operations
        public static ITween DoFade(CanvasGroup target, float endValue, float duration)
        {
            return TweenManager.Instance.DoFade(target, endValue, duration);
        }

        public static ITween DoFade(
            CanvasGroup target,
            float startValue,
            float endValue,
            float duration
        )
        {
            target.alpha = startValue;
            return TweenManager.Instance.DoFade(target, endValue, duration);
        }

        public static ITween DoFade(Graphic target, float endValue, float duration)
        {
            return TweenManager.Instance.DoFade(target, endValue, duration);
        }

        public static ITween DoFade(
            Graphic target,
            float startValue,
            float endValue,
            float duration
        )
        {
            target.SetAlpha(startValue);
            return TweenManager.Instance.DoFade(target, endValue, duration);
        }

        // Movement operations
        public static ITween DoAnchoredPos(RectTransform target, Vector2 endValue, float duration)
        {
            return TweenManager.Instance.DoAnchoredPos(target, endValue, duration);
        }

        public static ITween DoAnchoredPos(
            RectTransform target,
            Vector2 startValue,
            Vector2 endValue,
            float duration
        )
        {
            target.SetAnchorPosition(startValue);
            return TweenManager.Instance.DoAnchoredPos(target, endValue, duration);
        }

        // Scale operations
        public static ITween DoScale(Transform transform, Vector3 targetScale, float duration)
        {
            return TweenManager.Instance.DoScale(transform, targetScale, duration);
        }

        public static ITween DoScale(
            Transform target,
            Vector3 startValue,
            Vector3 endValue,
            float duration
        )
        {
            target.SetLocalScale(startValue);
            return TweenManager.Instance.DoScale(target, endValue, duration);
        }

        public static ITween DoScale(Transform target, float endValue, float duration)
        {
            return TweenManager.Instance.DoScale(target, endValue, duration);
        }

        public static ITween DoScale(
            Transform target,
            float startValue,
            float endValue,
            float duration
        )
        {
            Debug.Log($"Do Scale with startValue: {startValue} and endValue: {endValue}");
            target.SetLocalScale(new Vector3(startValue, startValue, startValue));
            return TweenManager.Instance.DoScale(target, endValue, duration);
        }

        public static ITween DoRotate(Transform target, Vector3 endValue, float duration)
        {
            return TweenManager.Instance.DoRotate(target, endValue, duration);
        }

        public static ITween DoRotate(
            Transform target,
            Vector3 startValue,
            Vector3 endValue,
            float duration
        )
        {
            target.SetRotationEuler(startValue);
            return TweenManager.Instance.DoRotate(target, endValue, duration);
        }

        // Sequence operations
        public static ISequence Sequence()
        {
            return TweenManager.Instance.CreateSequence();
        }

        // Utility operations
        public static void KillAll()
        {
            TweenManager.Instance.KillAllTweens();
        }
    }
}
