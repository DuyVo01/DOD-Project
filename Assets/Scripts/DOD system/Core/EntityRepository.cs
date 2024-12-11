using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRepository<T> : MonoBehaviour, IEntityRepository<T>
    where T : IEntity
{
    public ChunkArray<T> DataEntities { get; set; }

    private int initialCapacity;

    private void Awake()
    {
        initialCapacity = 1;
        DataEntities = new ChunkArray<T>(initialCapacity);
    }

    public void AddEntity(T entity)
    {
        DataEntities.Add(entity);
    }

    public void PopulateEntityId(int indexId)
    {
        DataEntities.Get(indexId).EntityID = indexId;
    }
}
