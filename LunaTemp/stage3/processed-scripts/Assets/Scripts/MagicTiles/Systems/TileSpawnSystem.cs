using UnityEngine;

public struct TileSpawnSystem : IGameSystem
{
    public void SpawnTileNote(
        int entityId,
        ref PerfectLineData perfectLine,
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteTransformData musicNoteTransformData
    )
    {
        // Calculate lane width once
        float totalWidth = perfectLine.TopRight.x - perfectLine.TopLeft.x;
        float laneWidth = totalWidth / 4;
        float halfLaneWidth = laneWidth / 2f;

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
        Debug.Log($"Bottom Left [{entityId}]: {musicNoteTransformData.BottomLeft.Get(entityId)}");
        Debug.Log($"Bottom Right [{entityId}]: {musicNoteTransformData.BottomRight.Get(entityId)}");
    }
}
