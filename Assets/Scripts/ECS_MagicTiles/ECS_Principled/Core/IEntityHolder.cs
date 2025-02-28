using UnityEngine;

namespace ECS_MagicTile
{
    public interface IEntityHolder
    {
        int EntityId { get; }

        void SetEntityId(int id);
    }
}
