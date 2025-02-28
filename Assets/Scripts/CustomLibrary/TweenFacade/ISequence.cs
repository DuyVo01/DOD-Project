namespace Facade.Tweening
{
    public interface ISequence
    {
        ISequence Chain(ITween tween);
        ISequence Delay(float interval);
        ISequence Join(ITween tween);
        ISequence SetLoops(int loops, LoopType loopType = LoopType.Restart);
        ISequence OnComplete(System.Action callback);
        void Kill();
        bool IsActive();
    }
}
