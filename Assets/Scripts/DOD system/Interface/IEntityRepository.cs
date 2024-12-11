using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityRepository<T>
{
    public ChunkArray<T> DataEntities { get; set; }
    public void AddEntity(T entity);

}
