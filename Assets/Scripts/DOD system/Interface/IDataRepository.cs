using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataRepository<T>
{
    public ChunkArray<T> DataEntities { get; set; }
}
