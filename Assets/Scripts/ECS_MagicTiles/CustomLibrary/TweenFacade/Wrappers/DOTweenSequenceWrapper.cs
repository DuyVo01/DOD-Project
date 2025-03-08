using System;
using DG.Tweening;

namespace Facade.Tweening
{
    public class DOTweenSequenceWrapper : ISequence
    {
        private Sequence _sequence;

        public DOTweenSequenceWrapper()
        {
            _sequence = DOTween.Sequence();
        }

        public ISequence Chain(ITween tween)
        {
            if (tween is DOTweenWrapper doTween)
            {
                _sequence.Append(doTween.InternalTween);
            }
            return this;
        }

        public ISequence Join(ITween tween)
        {
            if (tween is DOTweenWrapper doTween)
            {
                _sequence.Join(doTween.InternalTween);
            }
            return this;
        }

        public ISequence Delay(float interval)
        {
            _sequence.AppendInterval(interval);
            return this;
        }

        public ISequence SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            _sequence.SetLoops(
                loops,
                loopType == LoopType.Yoyo ? DG.Tweening.LoopType.Yoyo : DG.Tweening.LoopType.Restart
            );
            return this;
        }

        public ISequence OnComplete(Action callback)
        {
            _sequence.OnComplete(() => callback?.Invoke());
            return this;
        }

        public void Kill()
        {
            _sequence.Kill();
        }

        public bool IsActive() => _sequence.IsActive();
    }
}
