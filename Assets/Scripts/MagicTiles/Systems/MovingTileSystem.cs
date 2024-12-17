using UnityEngine;

public struct MovingTileSystem : IGameSystem
{
    public void MovingTile(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteFillerData musicNoteFillerData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        Vector2 newPos = Vector2.zero;

        newPos.x = musicNoteTransformData.positions.Get(entityId).x;
        newPos.y =
            musicNoteTransformData.positions.Get(entityId).y
            - GlobalGameSetting.Instance.generalSetting.gameSpeed * Time.deltaTime;
        musicNoteTransformData.positions.Set(entityId, newPos);
        if (musicNoteStateData.noteTypes.Get(entityId) == MusicNoteType.LongNote)
        {
            musicNoteFillerData.Positions.Set(entityId, newPos);
        }
    }
}
