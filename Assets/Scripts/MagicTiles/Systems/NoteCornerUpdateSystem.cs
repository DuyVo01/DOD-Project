using UnityEngine;

public struct NoteCornerUpdateSystem : IGameSystem
{
    public void UpdateCorners()
    {
        ref var noteEntityManager = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityManager.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        for (int entityId = 0; entityId < noteEntityManager.EntityCount; entityId++)
        {
            if (!noteEntityManager.IsEntityActive(entityId))
                continue;

            Vector3 position = musicNoteTransformData.positions.Get(entityId);
            Vector2 size = musicNoteTransformData.sizes.Get(entityId);

            float halfWidth = size.x / 2f;
            float halfHeight = size.y / 2f;

            // Update all corners based on current position
            musicNoteTransformData.TopLeft.Set(
                entityId,
                new Vector2(position.x - halfWidth, position.y + halfHeight)
            );
            musicNoteTransformData.TopRight.Set(
                entityId,
                new Vector2(position.x + halfWidth, position.y + halfHeight)
            );
            musicNoteTransformData.BottomLeft.Set(
                entityId,
                new Vector2(position.x - halfWidth, position.y - halfHeight)
            );
            musicNoteTransformData.BottomRight.Set(
                entityId,
                new Vector2(position.x + halfWidth, position.y - halfHeight)
            );
        }
    }
}
