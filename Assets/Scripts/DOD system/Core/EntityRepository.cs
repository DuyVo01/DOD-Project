using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRepository<T> : MonoBehaviour, IDataRepository<T>
    where T : IDataEntity
{
    public static EntityRepository<T> Instance { get; private set; }
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
}
