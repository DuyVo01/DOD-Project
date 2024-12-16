using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnityTransformBridge : IBridge
{
    public void SyncTransformToUnity(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData
    )
    {
        ref var presenterManager = ref PresenterManagerRepository.GetManager<
            PresenterManager<MusicNotePresenterTemplateSO>
        >(PresenterManagerType.MusicNotePresenterManager);

        GameObject presenter;

        presenter = presenterManager.GetOrCreatePresenter(entityId);

        presenter.transform.position = musicNoteTransformData.positions.Get(entityId);
        presenter.transform.localScale = musicNoteTransformData.sizes.Get(entityId);
    }
}
