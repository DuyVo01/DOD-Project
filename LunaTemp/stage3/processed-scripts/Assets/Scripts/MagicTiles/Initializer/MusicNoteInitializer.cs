using UnityEngine;

public struct MusicNoteInitializer
{
    public static void Initialize()
    {
        ref var transfromUpdateSystem = ref SystemRepository.GetSystem<TransformUpdateSystem>();
        ref var tileSpawnSystem = ref SystemRepository.GetSystem<TileSpawnSystem>();

        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        ref var musicNoteMidiData = ref noteEntityGroup.GetComponent<MusicNoteMidiData>(
            MusicNoteComponentType.MusicNoteMidiData
        );

        ref var musicNoteStateData = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        ref var noteStateSystem = ref SystemRepository.GetSystem<NoteStateSystem>();

        ref var perfectLineData = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            if (!noteEntityGroup.IsEntityActive(entityId))
            {
                continue;
            }
            noteStateSystem.NoteStateDeterminer(
                entityId,
                ref musicNoteMidiData,
                ref musicNoteStateData
            );
            transfromUpdateSystem.SyncTransformScale(
                entityId,
                ref musicNoteMidiData,
                ref musicNoteTransformData,
                ref musicNoteStateData
            );
            tileSpawnSystem.SpawnTileNote(
                entityId,
                ref perfectLineData,
                ref musicNoteMidiData,
                ref musicNoteTransformData
            );
        }

        ref var introNoteData = ref SingletonComponentRepository.GetComponent<IntroNoteData>(
            SingletonComponentType.IntroNote
        );
        ref var introNoteInitSystem = ref SystemRepository.GetSystem<IntroNoteInitSystem>();

        introNoteInitSystem.PrepareIntroNote(ref introNoteData, ref perfectLineData);
    }
}
