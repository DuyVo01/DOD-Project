using System.Collections;
using System.Collections.Generic;
using ECS_MagicTile;
using UnityEngine;

namespace ECS_MagicTile.Components
{
    /// <summary>
    /// Tags an entity as the Perfect Line - implemented as a singleton
    /// </summary>
    public struct PerfectLineTagComponent : IComponent, ISingletonFlag
    {
        public float PerfectLineWidth;
    }
}
