using UnityEngine;

public struct LaneLineBridge : IBridge
{
    private ChunkArray<GameObject> cachedLaneLinePresenter;
    private ChunkArray<SpriteRenderer> cachedSpriteRenderers;

    private LaneLineBridge(bool fake)
    {
        ref var laneLinePresenter = ref PresenterManagerRepository.GetManager<PresenterManager>(
            PresenterManagerType.LaneLinePresenterManager
        );

        ref var laneLineEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<LaneLineComponentType>
        >(EntityType.LaneLineEntityGroup);

        ref var lanelineData = ref laneLineEntityGroup.GetComponent<LaneLineData>(
            LaneLineComponentType.LaneLineData
        );

        cachedLaneLinePresenter = new ChunkArray<GameObject>(laneLineEntityGroup.EntityCount);
        cachedSpriteRenderers = new ChunkArray<SpriteRenderer>(laneLineEntityGroup.EntityCount);

        GameObject presenterGO;
        for (int entityId = 0; entityId < laneLineEntityGroup.EntityCount; entityId++)
        {
            presenterGO = laneLinePresenter.GetOrCreatePresenter(entityId);
            cachedLaneLinePresenter.Add(presenterGO);
            cachedSpriteRenderers.Add(presenterGO.GetComponent<SpriteRenderer>());
            SyncTransform(entityId, ref lanelineData);
        }
    }

    public static LaneLineBridge Create()
    {
        return new LaneLineBridge(true);
    }

    public void SyncTransform(int entityId, ref LaneLineData landLineData)
    {
        // Update position
        cachedLaneLinePresenter.Get(entityId).transform.position = landLineData.Positions.Get(
            entityId
        );

        // Update scale based on size
        Vector2 size = landLineData.Sizes.Get(entityId);
        Vector2 scale = Vector2.one;

        // Get the sprite size in world units
        var sprite = cachedSpriteRenderers.Get(entityId).sprite;
        if (sprite != null)
        {
            Vector2 spriteSize = sprite.bounds.size;
            scale.x = size.x / spriteSize.x;
            scale.y = size.y / spriteSize.y;
        }

        cachedLaneLinePresenter.Get(entityId).transform.localScale = new Vector3(
            scale.x,
            scale.y,
            1f
        );
    }
}
