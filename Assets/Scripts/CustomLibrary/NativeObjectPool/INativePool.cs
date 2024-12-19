using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INativePool<T>
    where T : class
{
    T Get();
    void Return(T item);
    void Clear();
}
