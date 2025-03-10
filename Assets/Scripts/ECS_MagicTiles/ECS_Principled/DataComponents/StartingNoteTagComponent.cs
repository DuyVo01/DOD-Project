using ECS_MagicTile;
using UnityEngine;

namespace ECS_MagicTile.Components
{
    /// <summary>
    /// Tags an entity as the Starting Note - implemented as a singleton
    /// </summary>
    public struct StartingNoteTagComponent : IComponent, ISingletonFlag
    {
        public int initalLane;
    }
}
