using System;
using System.Collections.Generic;

namespace ECS_MagicTile
{
    /// <summary>
    /// Marker interface for components that flag an entity as a singleton
    /// This allows efficient storage and retrieval outside the regular chunk system
    /// </summary>
    public interface ISingletonFlag : IComponent
    {
        // This is just a marker interface - no methods required
    }
}
