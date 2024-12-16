using UnityEngine;

public struct MovingTileSystem : IGameSystem
{
    public void MovingTile(int entityId, ref MusicNoteTransformData musicNoteTransformData)
    {
        Vector2 newPos = Vector2.zero;

        newPos.x = musicNoteTransformData.positions.Get(entityId).x;
        newPos.y =
            musicNoteTransformData.positions.Get(entityId).y
            - GlobalGameSetting.Instance.generalSetting.gameSpeed * Time.deltaTime;
        musicNoteTransformData.positions.Set(entityId, newPos);
    }
}
