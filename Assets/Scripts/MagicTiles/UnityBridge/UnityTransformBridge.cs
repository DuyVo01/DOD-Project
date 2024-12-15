using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnityTransformBridge : IBridge
{
    public void SyncTransformToUnity()
    {
        ref var entityGroup = ref EntityRepository.GetEGroup<EntityGroup<MusicNoteComponentType>>(
            EntityType.NoteEntityGroup
        );
        ref var transformData = ref entityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );
        ref var presenterManager = ref SingletonComponentRepository.GetComponent<PresenterManager>(
            SingletonComponentType.MusicNotePresenterManager
        );

        GameObject presenter;

        for (int entityId = 0; entityId < entityGroup.EntityCount; entityId++)
        {
            presenter = presenterManager.GetOrCreatePresenter(entityId);

            presenter.transform.position = transformData.positions.Get(entityId);
            presenter.transform.localScale = transformData.sizes.Get(entityId);
        }
    }
}
