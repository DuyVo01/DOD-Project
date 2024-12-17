using UnityEngine;

public struct PresenterManager : IPresenterManager
{
    private GameObject[] presenters;
    private Transform parent;
    private GameObject @base;

    public PresenterManager(int capacity, Transform parent, GameObject @base)
    {
        this.presenters = new GameObject[capacity];
        this.parent = parent;
        this.@base = @base;
    }

    public GameObject GetOrCreatePresenter(int entityId)
    {
        if (presenters[entityId] == null)
        {
            presenters[entityId] = GameObject.Instantiate(@base, parent);
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
