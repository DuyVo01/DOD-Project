using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_MagicTile
{
    // Manages all archetype storages in our game
    public class StorageManager
    {
        // Maps archetype hashes to their storage
        private readonly Dictionary<int, ArchetypeStorage> storages;
        private readonly Dictionary<int, List<ArchetypeStorage>> aspectStorages;

        public StorageManager()
        {
            storages = new Dictionary<int, ArchetypeStorage>();
            aspectStorages = new Dictionary<int, List<ArchetypeStorage>>();

            // First, create regular storages
            foreach (var archetype in Archetype.Registry.GetAllArchetypes())
            {
                if (!IsAspectArchetype(archetype))
                {
                    storages[archetype.GetHash()] = new ArchetypeStorage(archetype);
                }

                if (IsAspectArchetype(archetype))
                {
                    // Find all archetypes that contain these components
                    var matchingStorages = FindMatchingStorages(archetype);
                    aspectStorages[archetype.GetHash()] = matchingStorages;
                }
            }
        }

        private bool IsAspectArchetype(Archetype archetype)
        {
            // You could mark aspect archetypes explicitly, or use a naming convention,
            // or determine based on component count
            return archetype.IsAspect;
        }

        private List<ArchetypeStorage> FindMatchingStorages(Archetype aspectArchetype)
        {
            var matchingStorages = new List<ArchetypeStorage>();
            var requiredComponents = aspectArchetype.GetTypes();

            foreach (var storage in storages.Values)
            {
                // Check if this storage's archetype contains all required components
                bool matches = true;
                foreach (var component in requiredComponents)
                {
                    if (!HasComponent(storage, component))
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                {
                    matchingStorages.Add(storage);
                }
            }

            return matchingStorages;
        }

        // Get storage for a specific archetype
        public ArchetypeStorage GetStorage(Archetype archetype)
        {
            if (!storages.TryGetValue(archetype.GetHash(), out var storage))
            {
                throw new InvalidOperationException($"No storage found for archetype {archetype}");
            }
            return storage;
        }

        private bool HasComponent(ArchetypeStorage storage, ComponentType componentType)
        {
            try
            {
                // We try to get the component array - if it exists, the storage has this component
                Array componentArray = storage.GetComponentArrayRaw(componentType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
