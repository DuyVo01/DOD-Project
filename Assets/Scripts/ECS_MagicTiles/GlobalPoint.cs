using System;
using System.Collections.Generic;
using ECS_Core;
using UnityEngine;

public class GlobalPoint : PersistentSingleton<GlobalPoint>
{
    private World world;

    protected override void OnAwake()
    {
        base.OnAwake();

        try
        {
            InitializeECS();
            CreateTemplates();
            CreateSingletons();
            CreateEntity();
            RegisterSystems();
            SystemManager.InitializeSystems(world);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to initialize game: {e}");
            // Handle initialization failure
        }
    }

    private void Update()
    {
        SystemManager.UpdateSystems();
    }

    private void InitializeECS()
    {
        world = new World();
        World.SetActiveWorld(world);
    }

    private void RegisterSystems()
    {
        SystemManager.RegisterSystem(new MockDebugSystem());
    }

    private void CreateTemplates()
    {
        world.CreateTemplate(
            "Mock",
            new Dictionary<ComponentType, object>
            {
                {
                    ComponentType.Of<MockComponentData>(),
                    new MockComponentData { Value = 1 }
                },

                // Add other components
            }
        );
    }

    private void CreateEntity()
    {
        int entityId = world.CreateEntityFromTemplate("Mock");
        int entityIds = world.CreateEntityFromTemplate("Mock");

        world.ModifyPendingComponent(
            entityId,
            (ref MockComponentData value) =>
            {
                value.Value = 10;
            }
        );

        world.ModifyPendingComponent(
            entityIds,
            (ref MockComponentData value) =>
            {
                value.Value = 12;
            }
        );

        world.UpdateEntity(entityId);
        world.UpdateEntity(entityIds);
    }

    private void CreateSingletons() { }

    protected void OnDestroy()
    {
        SystemManager.Clear();
    }
}
