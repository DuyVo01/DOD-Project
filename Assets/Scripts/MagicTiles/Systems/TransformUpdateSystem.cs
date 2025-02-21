using System.Linq;
using UnityEngine;

public struct TransformUpdateSystem : IGameSystem
{
    public void SyncTransformScale(
        int entityId,
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        ref var noteEntityManager = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        //

        Vector3 transformScale = Vector3.zero;

        float scaleX = GlobalGameSetting.Instance.perfectLineSettingSO.PerfectLineWidth() / 4;

        float scaleY = MagicTileHelper.CalculateScaleY(
            musicNoteStateData.noteTypes.Get(entityId),
            scaleX,
            musicNoteMidiData.Durations[entityId]
        );

        transformScale.x = scaleX;
        transformScale.y = scaleY;

        musicNoteTransformData.sizes.Set(entityId, transformScale);
    }
}
