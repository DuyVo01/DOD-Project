using UnityEngine;

public struct PresenterManager<T> : IPresenterManager
    where T : BasePresenterTemplate
{
    private GameObject[] presenters;
    private Transform parent;
    private T template;

    public PresenterManager(int capacity, Transform parent, T template)
    {
        this.presenters = new GameObject[capacity];
        this.parent = parent;
        this.template = template;
    }

    public GameObject GetOrCreatePresenter(int entityId)
    {
        if (presenters[entityId] == null)
        {
            presenters[entityId] = GameObject.Instantiate(template.BasePrefab, parent);
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
