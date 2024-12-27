using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComponentObjectPool
{
    public interface IObjectPool<T>
        where T : Component
    {
        T Get();
        void Return(T item);
        void Prewarm(int count);
        void Clear();
    }
}
