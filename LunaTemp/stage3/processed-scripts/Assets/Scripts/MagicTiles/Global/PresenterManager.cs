using UnityEngine;

public struct PresenterManager : IPresenterManager
{
    private readonly GameObject[] presenters;
    private readonly Transform parent;
    private readonly GameObject @base;

    public PresenterManager(int capacity, GameObject @base, Transform parent = null)
    {
        this.presenters = new GameObject[capacity];
        this.parent = parent;
        this.@base = @base;
    }

    public GameObject GetOrCreatePresenter(int entityId)
    {
        if (presenters[entityId] == null)
        {
            if (parent == null)
            {
                presenters[entityId] = GameObject.Instantiate(@base);
            }
            else
            {
                presenters[entityId] = GameObject.Instantiate(@base, parent);
            }
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
