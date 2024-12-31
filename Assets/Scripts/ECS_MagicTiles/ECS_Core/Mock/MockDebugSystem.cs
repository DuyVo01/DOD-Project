using ECS_Core;
using UnityEngine;

public struct MockDebugSystem : ECS_Core.IGameSystem
{
    public bool AutoUpdate { get; set; }
    public ArchetypeManager ArchetypeManager { get; set; }

    public void Cleanup()
    {
        //
    }

    public void Initialize()
    {
        AutoUpdate = true;
    }

    public void Start()
    {
        //
    }

    public void Update() { }
}
