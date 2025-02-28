namespace Facade.Tweening
{
    public interface ITween
    {
        ITween SetEase(EaseType ease);
        ITween SetDelay(float delay);
        ITween SetLoops(int loops, LoopType loopType = LoopType.Restart);
        ITween OnComplete(System.Action onComplete);
        void Kill();
        bool IsActive();
    }
}
