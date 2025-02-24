using UnityEngine;

public struct IntroNoteTransformBridge : IBridge
{
    private GameObject cachedIntroNotePresenter;

    private IntroNoteTransformBridge(bool fake)
    {
        ref var introNotePresenter = ref PresenterManagerRepository.GetManager<PresenterManager>(
            PresenterManagerType.IntroNotePresenterManager
        );
        GameObject go = introNotePresenter.GetOrCreatePresenter(0);
        cachedIntroNotePresenter = go;
    }

    public void SyncIntroNoteTransform(ref IntroNoteData introNoteData)
    {
        cachedIntroNotePresenter.transform.position = introNoteData.Position;
        cachedIntroNotePresenter.SetActive(introNoteData.isActive);
    }

    public static IntroNoteTransformBridge Create()
    {
        return new IntroNoteTransformBridge(true);
    }
}
