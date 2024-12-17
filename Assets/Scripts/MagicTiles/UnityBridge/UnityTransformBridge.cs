using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnityTransformBridge : IBridge
{
    private ChunkArray<GameObject> cachedPresenters;
    private ChunkArray<SpriteRenderer> cachedPresenterSprites;

    private UnityTransformBridge(bool fake = true)
    {
        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var presenterManager = ref PresenterManagerRepository.GetManager<PresenterManager>(
            PresenterManagerType.MusicNotePresenterManager
        );

        cachedPresenters = new ChunkArray<GameObject>(noteEntityGroup.EntityCount);
        cachedPresenterSprites = new ChunkArray<SpriteRenderer>(noteEntityGroup.EntityCount);

        GameObject presenterGO;

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            presenterGO = presenterManager.GetOrCreatePresenter(entityId);

            cachedPresenters.Add(presenterGO);
            cachedPresenterSprites.Add(presenterGO.GetComponent<SpriteRenderer>());
        }
    }

    public static UnityTransformBridge Create()
    {
        return new UnityTransformBridge(true);
    }

    public void SyncNoteTransformToUnity(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData
    )
    {
        cachedPresenters.Get(entityId).transform.position = musicNoteTransformData.positions.Get(
            entityId
        );
        cachedPresenters.Get(entityId).transform.localScale = musicNoteTransformData.sizes.Get(
            entityId
        );
    }
}
