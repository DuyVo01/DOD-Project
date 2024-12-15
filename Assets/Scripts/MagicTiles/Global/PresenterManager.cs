using UnityEngine;

public struct PresenterManager : IDataComponent
{
    private GameObject[] presenters;
    private Transform parent;
    private PresenterTemplateSO template;

    public PresenterManager(int capacity, Transform parent, PresenterTemplateSO template)
    {
        this.presenters = new GameObject[capacity];
        this.parent = parent;
        this.template = template;
    }

    public GameObject GetOrCreatePresenter(int entityId)
    {
        if (presenters[entityId] == null)
        {
            presenters[entityId] = GameObject.Instantiate(template.basePrefab, parent);
        }
        return presenters[entityId];
    }

    public void Cleanup()
    {
        for (int i = 0; i < presenters.Length; i++)
        {
            if (presenters[i] != null)
            {
                GameObject.Destroy(presenters[i]);
            }
        }
    }
}
