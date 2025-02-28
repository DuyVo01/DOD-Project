using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Facade.Tweening
{
    public class TweenManager : PersistentSingleton<TweenManager>
    {
        public enum TweenLibrary
        {
            DOTween,
            PrimeTween,
        }

        [SerializeField]
        private TweenLibrary _currentLibrary = TweenLibrary.DOTween;

        protected override void OnAwake()
        {
            base.OnAwake();

            InitializeTweenLibrary();
        }

        private void InitializeTweenLibrary()
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                if (!DG.Tweening.DOTween.instance)
                {
                    DG.Tweening.DOTween.Init();
                }
            }
            // PrimeTween doesn't need explicit initialization
        }

        #region Tweens
        // Fade a CanvasGroup
        public ITween DoFade(CanvasGroup target, float endValue, float duration)
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                var tween = target.DOFade(endValue, duration);
                return new DOTweenWrapper(tween);
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }

        public ITween DoFade(Graphic target, float endValue, float duration)
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                var tween = target.DOFade(endValue, duration);
                return new DOTweenWrapper(tween);
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }

        // Move a RectTransform (UI element)
        public ITween DoAnchoredPos(RectTransform target, Vector2 endValue, float duration)
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                var tween = target.DOAnchorPos(endValue, duration);
                return new DOTweenWrapper(tween);
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }

        // Scale a Transform
        public ITween DoScale(Transform target, Vector3 endValue, float duration)
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                var tween = target.DOScale(endValue, duration);
                return new DOTweenWrapper(tween);
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }

        // Scale a transform uniformly
        public ITween DoScale(Transform target, float endValue, float duration)
        {
            return DoScale(target, new Vector3(endValue, endValue, endValue), duration);
        }

        public ITween DoRotate(Transform target, Vector3 endValue, float duration)
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                var tween = target.DORotate(endValue, duration);
                return new DOTweenWrapper(tween);
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }
        #endregion

        #region Sequence
        // Create a sequence
        public ISequence CreateSequence()
        {
            if (_currentLibrary == TweenLibrary.DOTween)
            {
                return new DOTweenSequenceWrapper();
            }
            else
            {
                throw new System.Exception("Unsupported tween library");
            }
        }
        #endregion

        #region Utility Operations

        // Kill all tweens
        public void KillAllTweens()
        {
            if (_currentLibrary == TweenLibrary.PrimeTween)
            {
                DG.Tweening.DOTween.KillAll();
            }
        }

        // Switch libraries at runtime if needed
        public void SwitchLibrary(TweenLibrary library)
        {
            if (_currentLibrary != library)
            {
                // Kill all tweens before switching
                KillAllTweens();

                _currentLibrary = library;
                InitializeTweenLibrary();

                Debug.Log($"Switched tween library to {library}");
            }
        }

        #endregion
    }
}
