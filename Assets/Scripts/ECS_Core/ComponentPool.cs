using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Core
{
    public class ComponentPool<T>
        where T : struct, IComponent
    {
        private const int DEFAULT_POOL_SIZE = 128;
    }
}
