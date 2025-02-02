using System.Linq;
using UnityEngine;

namespace ECS_MagicTile
{
    public class EntityIdHolder : MonoBehaviour, IEntityHolder
    {
        public int EntityId { get; private set; }

        public void SetEntityId(int id)
        {
            EntityId = id;
            // Optional: Make the GameObject name more readable without losing the ID reference
            gameObject.name = $"{id}_{gameObject.name.Split('_').LastOrDefault() ?? "entity"}";
        }
    }
}
