using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityViewFactory
{
    private readonly Dictionary<int, GameObject> entityViews = new();
    private readonly GameObject prefabSource;
    private readonly Transform viewRoot;

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

        var view = GameObject.Instantiate(prefab, viewRoot);
        view.name = $"{entityId}_{nameOnCreation}";
        entityViews[entityId] = view;
        return view;
    }

    public GameObject GetView(int entityId)
    {
        return entityViews.TryGetValue(entityId, out var view) ? view : null;
    }
}
