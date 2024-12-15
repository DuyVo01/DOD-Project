using System.Linq;
using UnityEngine;

public struct TransformUpdateSystem : IGameSystem
{
    public void SyncTransformScale()
    {
        ref var noteEntityManager = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);
        ref var musicNoteMidiData = ref noteEntityManager.GetComponent<MusicNoteMidiData>(
            MusicNoteComponentType.MusicNoteMidiData
        );
        ref var musicNoteTransformData = ref noteEntityManager.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );
        ref var perfectLine = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        //
        float shortestDuration = musicNoteMidiData.Durations.Min();

        Vector3 transformScale = Vector3.zero;
        for (int entityId = 0; entityId < noteEntityManager.EntityCount; entityId++)
        {
            float scaleX = GlobalGameSetting.Instance.perfectLineSettingSO.PerfectLineWidth() / 4;
            float scaleY = MagicTileHelper.ConvertDurationToAppropiateScaleY(
                shortestDuration,
                GlobalGameSetting.Instance.generalSetting.baseScaleYForNote,
                musicNoteMidiData.Durations[entityId]
            );

            transformScale.x = scaleX;
            transformScale.y = scaleY;

            musicNoteTransformData.sizes.Set(entityId, transformScale);
        }
    }
}
