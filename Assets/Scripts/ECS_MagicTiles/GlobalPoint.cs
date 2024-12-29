using System;
using System.Collections.Generic;
using ECS_Core;
using UnityEngine;

public class GlobalPoint : PersistentSingleton<GlobalPoint>
{
    public PrefabSourceSO prefabSourceSO;
    public PerfectLineSettingSO perfectLineSettingSO;
    public TextAsset midiContent;
    public float gameSpeed;
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

    private void Start()
    {
        SystemManager.SystemStart();
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
        SystemManager.RegisterSystem(new MusicNoteCreationSystem());
    }

    private void CreateTemplates()
    {
        world.CreateTemplate(
            "MusicNote",
            new Dictionary<ComponentType, object>
            {
                { ComponentType.Of<MusicNoteComponent>(), new MusicNoteComponent() },
                { ComponentType.Of<TransformComponent>(), new TransformComponent() },
            }
        );
    }

    private void CreateEntity() { }

    private void CreateSingletons()
    {
        int perfectLineEntity = world.CreateEntity();
        world.AddComponent(perfectLineEntity, new SingletonFlag());
        world.AddComponent(perfectLineEntity, new PerfectLineTagComponent());
        world.AddComponent(
            perfectLineEntity,
            new CornerComponent
            {
                TopLeft = perfectLineSettingSO.TopLeft,
                TopRight = perfectLineSettingSO.TopRight,
                BottomLeft = perfectLineSettingSO.BottomLeft,
                BottomRight = perfectLineSettingSO.BottomRight,
            }
        );
        world.UpdateEntity(perfectLineEntity);
    }

    protected void OnDestroy()
    {
        SystemManager.Clear();
    }
}
