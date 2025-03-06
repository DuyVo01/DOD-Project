using System;
using System.Collections;
using System.Collections.Generic;
using ComponentCache;
using ECS_MagicTile.Components;
using UnityEngine;

namespace ECS_MagicTile
{
    public class GameInGameSystem : MonoBehaviour, IGameSystem
    {
        [SerializeField]
        private GameObject handObject;
        ArchetypeStorage startingNoteStorage;
        TransformComponent[] startingNoteTransforms;

        private GlobalPoint globalPoint;

        public bool IsEnabled { get; set; } = true;
        public World World { get; set; }

        void Start()
        {
            handObject.RegisterComponent(handObject.GetComponent<RectTransform>());
            handObject.SetActive(false);
            globalPoint = GameObject.FindObjectOfType<GlobalPoint>();
            if (globalPoint == null)
            {
                Debug.LogError("GameInGameSystem: Could not find GlobalPoint!");
            }
        }

        public void RunInitialize()
        {
            handObject.SetActive(true);
            startingNoteStorage = World.GetStorage(Archetype.Registry.StartingNote);
            startingNoteTransforms = startingNoteStorage.GetComponents<TransformComponent>();

            handObject.transform.position = startingNoteTransforms[0].Position;
        }

        public void SetWorld(World world)
        {
            World = world;
        }

        public void RunUpdate(float deltaTime) { }

        public void RunCleanup() { }
    }
}
