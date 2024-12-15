using UnityEngine;

public struct MovingTileSystem : IGameSystem
{
    public void MovingTile()
    {
        ref var entityGroup = ref EntityRepository.GetEGroup<EntityGroup<MusicNoteComponentType>>(
            EntityType.NoteEntityGroup
        );

        ref var musicNoteTransformData = ref entityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        Vector2 newPos = Vector2.zero;
        for (int entityId = 0; entityId < entityGroup.EntityCount; entityId++)
        {
            newPos.x = musicNoteTransformData.positions.Get(entityId).x;
            newPos.y =
                musicNoteTransformData.positions.Get(entityId).y
                - GlobalGameSetting.Instance.generalSetting.gameSpeed * Time.deltaTime;
            musicNoteTransformData.positions.Set(entityId, newPos);
        }
    }
}
