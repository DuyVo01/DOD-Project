using System;
using DG.Tweening;

namespace Facade.Tweening
{
    public class DOTweenWrapper : ITween
    {
        private DG.Tweening.Tween _tween;

        public DOTweenWrapper(DG.Tweening.Tween tween)
        {
            _tween = tween;
        }

        public ITween SetEase(EaseType easeType)
        {
            _tween.SetEase(ConvertToEase(easeType));
            return this;
        }

        public ITween SetDelay(float delay)
        {
            _tween.SetDelay(delay);
            return this;
        }

        public ITween SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            _tween.SetLoops(loops, ConvertToLoopType(loopType));
            return this;
        }

        public ITween OnComplete(Action callback)
        {
            _tween.OnComplete(() => callback?.Invoke());
            return this;
        }

        public void Kill()
        {
            _tween.Kill();
        }

        private DG.Tweening.Ease ConvertToEase(EaseType easeType)
        {
            switch (easeType)
            {
                case EaseType.Linear:
                    return DG.Tweening.Ease.Linear;
                case EaseType.InSine:
                    return DG.Tweening.Ease.InSine;
                case EaseType.OutSine:
                    return DG.Tweening.Ease.OutSine;
                case EaseType.InOutSine:
                    return DG.Tweening.Ease.InOutSine;
                case EaseType.InQuad:
                    return DG.Tweening.Ease.InQuad;
                case EaseType.OutQuad:
                    return DG.Tweening.Ease.OutQuad;
                case EaseType.InOutQuad:
                    return DG.Tweening.Ease.InOutQuad;
                case EaseType.InCubic:
                    return DG.Tweening.Ease.InCubic;
                case EaseType.OutCubic:
                    return DG.Tweening.Ease.OutCubic;
                case EaseType.InOutCubic:
                    return DG.Tweening.Ease.InOutCubic;
                case EaseType.InQuart:
                    return DG.Tweening.Ease.InQuart;
                case EaseType.OutQuart:
                    return DG.Tweening.Ease.OutQuart;
                case EaseType.InOutQuart:
                    return DG.Tweening.Ease.InOutQuart;
                case EaseType.InQuint:
                    return DG.Tweening.Ease.InQuint;
                case EaseType.OutQuint:
                    return DG.Tweening.Ease.OutQuint;
                case EaseType.InOutQuint:
                    return DG.Tweening.Ease.InOutQuint;
                case EaseType.InExpo:
                    return DG.Tweening.Ease.InExpo;
                case EaseType.OutExpo:
                    return DG.Tweening.Ease.OutExpo;
                case EaseType.InOutExpo:
                    return DG.Tweening.Ease.InOutExpo;
                case EaseType.InCirc:
                    return DG.Tweening.Ease.InCirc;
                case EaseType.OutCirc:
                    return DG.Tweening.Ease.OutCirc;
                case EaseType.InOutCirc:
                    return DG.Tweening.Ease.InOutCirc;
                case EaseType.InBack:
                    return DG.Tweening.Ease.InBack;
                case EaseType.OutBack:
                    return DG.Tweening.Ease.OutBack;
                case EaseType.InOutBack:
                    return DG.Tweening.Ease.InOutBack;
                case EaseType.InElastic:
                    return DG.Tweening.Ease.InElastic;
                case EaseType.OutElastic:
                    return DG.Tweening.Ease.OutElastic;
                case EaseType.InOutElastic:
                    return DG.Tweening.Ease.InOutElastic;
                case EaseType.InBounce:
                    return DG.Tweening.Ease.InBounce;
                case EaseType.OutBounce:
                    return DG.Tweening.Ease.OutBounce;
                case EaseType.InOutBounce:
                    return DG.Tweening.Ease.InOutBounce;
                default:
                    return DG.Tweening.Ease.Linear;
            }
        }

        private DG.Tweening.LoopType ConvertToLoopType(LoopType loopType)
        {
            return loopType == LoopType.Yoyo
                ? DG.Tweening.LoopType.Yoyo
                : DG.Tweening.LoopType.Restart;
        }

        public bool IsActive() => _tween.IsActive();

        // Allow access to the internal tween for the sequence wrapper
        internal DG.Tweening.Tween InternalTween => _tween;
    }
}
