using UnityEngine;

public struct MovingTileSystem : IGameSystem
{
    public void MovingTile(
        int entityId,
        ref MusicNoteTransformData musicNoteTransformData,
        ref MusicNoteStateData musicNoteStateData
    )
    {
        if (musicNoteStateData.positionStates.Get(entityId) == MusicNotePositionState.OutOfScreen)
        {
            Debug.Log($"Entity {entityId} is out of bounds");
            return;
        }

        Vector2 newPos = Vector2.zero;

        float gameSpeed = GlobalGameSetting.Instance.generalSetting.gameSpeed;

        newPos.x = musicNoteTransformData.positions.Get(entityId).x;
        newPos.y = musicNoteTransformData.positions.Get(entityId).y - gameSpeed * Time.deltaTime;

        musicNoteTransformData.positions.Set(entityId, newPos);
    }
}
