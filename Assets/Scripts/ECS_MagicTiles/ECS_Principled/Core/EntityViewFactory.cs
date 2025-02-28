using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public class EntityViewFactory
    {
        private readonly Dictionary<int, GameObject> entityViews =
            new Dictionary<int, GameObject>();
        private readonly GameObject prefabSource;
        private readonly Transform viewRoot;

        public EntityViewFactory() { }

        public EntityViewFactory(GameObject prefabSource, Transform viewRoot)
        {
            this.prefabSource = prefabSource;
            this.viewRoot = viewRoot;
        }

        public GameObject GetOrCreateView(int entityId, string nameOnCreation = "")
        {
            if (entityViews.TryGetValue(entityId, out var existing))
            {
                return existing;
            }

            var prefab = prefabSource;

            GameObject view = GameObject.Instantiate(prefab, viewRoot);
            EntityIdHolder viewEntityIdHolder = view.AddComponent<EntityIdHolder>();
            viewEntityIdHolder.SetEntityId(entityId);
            entityViews[entityId] = view;
            return view;
        }

        public GameObject GetView(int entityId)
        {
            return entityViews.TryGetValue(entityId, out var view) ? view : null;
        }
    }
}
