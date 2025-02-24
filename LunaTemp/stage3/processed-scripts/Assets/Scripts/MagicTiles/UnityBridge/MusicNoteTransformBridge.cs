using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MusicNoteTransformBridge : IBridge
{
    private readonly ChunkArray<GameObject> cachedNotePresenters;
    private readonly ChunkArray<SpriteRenderer> cachedNotePresenterSprites;

    private readonly Dictionary<int, SpriteRenderer> cachedLongNoteFiller;

    private MusicNoteTransformBridge(bool fake)
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
        cachedLongNoteFiller = new Dictionary<int, SpriteRenderer>();

        GameObject presenterGO;

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            if (!noteEntityGroup.IsEntityActive(entityId))
            {
                continue;
            }

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
                cachedLongNoteFiller.Add(
                    entityId,
                    presenterGO.transform.GetChild(0).GetComponent<SpriteRenderer>()
                );
            }
        }
    }

    public static MusicNoteTransformBridge Create()
    {
        return new MusicNoteTransformBridge(true);
    }

    public void SyncNoteTransformToUnity(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData,
        ref MusicNoteFillerData musicNoteFillerData
    )
    {
        cachedNotePresenters.Get(entityId).transform.position =
            musicNoteTransformData.positions.Get(entityId);
        cachedNotePresenters.Get(entityId).transform.localScale = musicNoteTransformData.sizes.Get(
            entityId
        );

        if (
            musicNoteStateData.noteTypes.Get(entityId) == MusicNoteType.ShortNote
            && musicNoteStateData.interactiveStates.Get(entityId)
                == MusicNoteInteractiveState.Completed
        )
        {
            cachedNotePresenterSprites.Get(entityId).color = Color.black;
        }
        if (
            musicNoteStateData.noteTypes.Get(entityId) == MusicNoteType.LongNote
            && cachedLongNoteFiller.ContainsKey(entityId)
        )
        {
            cachedLongNoteFiller[entityId]
                .gameObject.SetActive(musicNoteFillerData.IsVisibles.Get(entityId));
            float fillerPercentage = musicNoteFillerData.FillPercent.Get(entityId);

            SpriteUtility.ScaleFromPivot(
                cachedLongNoteFiller[entityId],
                new Vector2(1, fillerPercentage),
                SpriteUtility.PivotPointXY.Bottom
            );
        }
    }
}
