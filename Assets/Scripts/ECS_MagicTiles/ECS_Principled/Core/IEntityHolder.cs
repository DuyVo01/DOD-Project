using UnityEngine;

namespace ECS_MagicTile
{
    public interface IEntityHolder
    {
        public int EntityId { get; }

        public void SetEntityId(int id);
    }
}
