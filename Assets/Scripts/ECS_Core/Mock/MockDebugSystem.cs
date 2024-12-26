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

    public void Update()
    {
        World
            .Active.Query<MockComponentData>()
            .ForEach(
                ArchetypeManager,
                (int entity, ref MockComponentData mockdata) =>
                {
                    Debug.Log(mockdata.Value);
                }
            );
    }
}
