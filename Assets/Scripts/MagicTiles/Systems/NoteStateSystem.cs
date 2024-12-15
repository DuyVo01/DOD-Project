using UnityEngine;

public struct NoteStateSystem : IGameSystem
{
    public void NoteStateUpdate()
    {
        ref var noteEntityManager = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);
        ref var perfectLineData = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        ref var musicNoteTransformData = ref noteEntityManager.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );
        ref var musicNoteStateData = ref noteEntityManager.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        float noteUpperY;
        float noteLowerY;
        float perfectLineUpperY = perfectLineData.TopLeft.y;
        float perfectLineLowerY = perfectLineData.BottomLeft.y;

        for (int entityId = 0; entityId < noteEntityManager.EntityCount; entityId++)
        {
            if (
                musicNoteStateData.positionStates.Get(entityId)
                == MusicNotePositionState.PassedPerfectLine
            )
            {
                continue;
            }
            noteUpperY = musicNoteTransformData.TopLeft.Get(entityId).y;
            noteLowerY = musicNoteTransformData.BottomLeft.Get(entityId).y;

            if (noteLowerY < perfectLineUpperY && noteUpperY > perfectLineLowerY)
            {
                musicNoteStateData.positionStates.Set(
                    entityId,
                    MusicNotePositionState.InlineWithPerfectLine
                );
            }
            else if (noteUpperY < perfectLineLowerY)
            {
                musicNoteStateData.positionStates.Set(
                    entityId,
                    MusicNotePositionState.PassedPerfectLine
                );

                Debug.Log($"Entity {entityId} has passed perfect line");
            }
        }
    }
}
