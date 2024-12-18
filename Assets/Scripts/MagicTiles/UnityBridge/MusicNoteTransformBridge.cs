using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicNoteTransformBridge : IBridge
{
    private ChunkArray<GameObject> cachedNotePresenters;
    private ChunkArray<SpriteRenderer> cachedNotePresenterSprites;

    private ChunkArray<GameObject> cachedLongNoteFiller;

    public void InitializeBridge()
    {
        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteStatedata = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        ref var shortNotePresenterManager =
            ref PresenterManagerRepository.GetManager<PresenterManager>(
                PresenterManagerType.MusicNotePresenterManager
            );

        ref var longNotePresenterManager =
            ref PresenterManagerRepository.GetManager<PresenterManager>(
                PresenterManagerType.LongNotePresenterManager
            );

        cachedNotePresenters = new ChunkArray<GameObject>(noteEntityGroup.EntityCount);
        cachedNotePresenterSprites = new ChunkArray<SpriteRenderer>(noteEntityGroup.EntityCount);

        GameObject presenterGO;

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            if (musicNoteStatedata.noteTypes.Get(entityId) == MusicNoteType.ShortNote)
            {
                presenterGO = shortNotePresenterManager.GetOrCreatePresenter(entityId);

                cachedNotePresenters.Add(presenterGO);
                cachedNotePresenterSprites.Add(presenterGO.GetComponent<SpriteRenderer>());
            }
            else if (musicNoteStatedata.noteTypes.Get(entityId) == MusicNoteType.LongNote)
            {
                presenterGO = longNotePresenterManager.GetOrCreatePresenter(entityId);

                cachedNotePresenters.Add(presenterGO);
                cachedNotePresenterSprites.Add(presenterGO.GetComponent<SpriteRenderer>());
            }
        }
    }

    public void SyncNoteTransformToUnity(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        cachedNotePresenters.Get(entityId).transform.position =
            musicNoteTransformData.positions.Get(entityId);
        cachedNotePresenters.Get(entityId).transform.localScale = musicNoteTransformData.sizes.Get(
            entityId
        );
    }
}
