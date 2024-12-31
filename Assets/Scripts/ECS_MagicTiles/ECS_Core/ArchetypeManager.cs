using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ECS_Core
{
    public class ArchetypeManager
    {
        private readonly Dictionary<int, Archetype> archetypes = new();
        private readonly World world;

        public ArchetypeManager(World world)
        {
            this.world = world;
        }

        public Archetype GetOrCreateArchetype(ComponentType[] types)
        {
            int hash = CalculateArchetypeHash(types);
            if (!archetypes.TryGetValue(hash, out var archetype))
            {
                archetype = new Archetype(types);
                archetypes[hash] = archetype;
                // Notify world that a new archetype was created
                world.GetQueryCache(types); // This will create a cache entry for the new archetype
            }

            return archetype;
        }

        public IEnumerable<Archetype> GetArchetypesWithComponents(ComponentType[] types)
        {
            Debug.Log(
                $"Searching archetypes for types: {string.Join(", ", types.Select(t => t.Type.Name))}"
            );
            Debug.Log($"Total archetypes: {archetypes.Count}"); // Add this debug
            foreach (var archetype in archetypes.Values)
            {
                Debug.Log(
                    $"Checking archetype with components: {string.Join(", ", archetype.ComponentTypes.Select(t => t.Type.Name))}"
                ); // Add this
                if (DoesArchetypeMatchQuery(archetype, types))
                {
                    yield return archetype;
                }
            }
        }

        private static bool DoesArchetypeMatchQuery(Archetype archetype, ComponentType[] queryTypes)
        {
            foreach (var queryType in queryTypes)
            {
                if (!archetype.HasComponent(queryType))
                {
                    Debug.Log($"Archetype missing component: {queryType.Type.Name}");
                    return false;
                }
            }
            return true;
        }

        private static int CalculateArchetypeHash(ComponentType[] types)
        {
            int hash = 17;
            foreach (var type in types.OrderBy(t => t.Id))
            {
                hash = hash * 31 + type.Id;
            }
            return hash;
        }

        public void CleanupEmptyArchetypes()
        {
            var emptyArchetypes = archetypes
                .Where(kvp => kvp.Value.Count == 0)
                .Select(kvp => kvp.Key)
                .ToList();

            if (emptyArchetypes.Count > 0)
            {
                foreach (var hash in emptyArchetypes)
                {
                    archetypes.Remove(hash);
                }
            }
        }
    }
}
