using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    public class EntityViewFactory
    {
        private readonly IndexedStorage<GameObject> entityViews = new IndexedStorage<GameObject>(
            64
        );
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
            if (entityViews.TryGetById(entityId, out var existing))
            {
                return existing;
            }

            var prefab = prefabSource;

            GameObject view = GameObject.Instantiate(prefab, viewRoot);
            EntityIdHolder viewEntityIdHolder = view.AddComponent<EntityIdHolder>();
            viewEntityIdHolder.SetEntityId(entityId);
            entityViews.SetById(entityId, view);
            return view;
        }

        public GameObject GetView(int entityId)
        {
            return entityViews.TryGetById(entityId, out var view) ? view : null;
        }
    }
}
