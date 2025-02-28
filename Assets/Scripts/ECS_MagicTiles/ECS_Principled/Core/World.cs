using System;
using UnityEngine;

namespace ECS_MagicTile
{
    public class World
    {
        private readonly StorageManager storageManager;
        private int nextEntityId = 1000; // Starting from 1000 to leave room for special IDs

        public World()
        {
            storageManager = new StorageManager();
        }

        // Creates an entity of a specific archetype
        private int CreateEntity()
        {
            int entityId = nextEntityId++;
            return entityId;
        }

        // Gets a specific storage for systems to use
        public ArchetypeStorage GetStorage(Archetype archetype)
        {
            return storageManager.GetStorage(archetype);
        }

        // Helper method to create and set up an entity in one go
        public int CreateEntityWithComponents(Archetype archetype, object[] components)
        {
            int entityId = CreateEntity();
            ArchetypeStorage storage = GetStorage(archetype);
            storage.Add(entityId, components);
            return entityId;
        }
    }
}
