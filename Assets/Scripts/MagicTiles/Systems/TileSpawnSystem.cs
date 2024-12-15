using UnityEngine;

public struct TileSpawnSystem : IGameSystem
{
    public void SpawnTileNote()
    {
        ref var noteEntityManager = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);
        ref var perfectLine = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        ref var musicNoteMidiData = ref noteEntityManager.GetComponent<MusicNoteMidiData>(
            MusicNoteComponentType.MusicNoteMidiData
        );
        ref var musicNoteTransformData = ref noteEntityManager.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        // Calculate lane width once
        float totalWidth = perfectLine.TopRight.x - perfectLine.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

        // Process all entities at once during spawn
        for (int entityId = 0; entityId < noteEntityManager.EntityCount; entityId++)
        {
            if (!noteEntityManager.IsEntityActive(entityId))
                continue;

            // Calculate final position
            float spawnX =
                perfectLine.TopLeft.x
                + (musicNoteMidiData.PositionIds[entityId] * laneWidth)
                + halfLaneWidth;

            float spawnY =
                perfectLine.TopLeft.y
                + (
                    musicNoteMidiData.TimeAppears[entityId]
                    * GlobalGameSetting.Instance.generalSetting.gameSpeed
                )
                + musicNoteTransformData.sizes.Get(entityId).y / 2f;

            // Set both MIDI data and transform position in one go
            musicNoteTransformData.positions.Set(entityId, new Vector3(spawnX, spawnY, 0));

            // Calculate and set corners if needed
            Vector2 currentSize = musicNoteTransformData.sizes.Get(entityId);
            float halfWidth = currentSize.x / 2f;
            float halfHeight = currentSize.y / 2f;

            musicNoteTransformData.TopLeft.Set(
                entityId,
                new Vector2(spawnX - halfWidth, spawnY + halfHeight)
            );
            musicNoteTransformData.TopRight.Set(
                entityId,
                new Vector2(spawnX + halfWidth, spawnY + halfHeight)
            );
            musicNoteTransformData.BottomLeft.Set(
                entityId,
                new Vector2(spawnX - halfWidth, spawnY - halfHeight)
            );
            musicNoteTransformData.BottomRight.Set(
                entityId,
                new Vector2(spawnX + halfWidth, spawnY - halfHeight)
            );

            Debug.Log($"Top Left [{entityId}]: {musicNoteTransformData.TopLeft.Get(entityId)}");
            Debug.Log($"Top Right [{entityId}]: {musicNoteTransformData.TopRight.Get(entityId)}");
            Debug.Log(
                $"Bottom Left [{entityId}]: {musicNoteTransformData.BottomLeft.Get(entityId)}"
            );
            Debug.Log(
                $"Bottom Right [{entityId}]: {musicNoteTransformData.BottomRight.Get(entityId)}"
            );
        }
    }
}
