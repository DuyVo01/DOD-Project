using System.Collections.Generic;
using System.Linq;

namespace ECS_Core
{
    public class ArchetypeManager
    {
        private Dictionary<int, Archetype> archetypes = new();

        public Archetype GetOrCreateArchetype(ComponentType[] types)
        {
            int hash = CalculateArchetypeHash(types);
            if (!archetypes.TryGetValue(hash, out var archetype))
            {
                archetype = new Archetype(types);
                archetypes[hash] = archetype;
            }

            return archetype;
        }

        public IEnumerable<Archetype> GetArchetypesWithComponents(ComponentType[] types)
        {
            foreach (var archetype in archetypes.Values)
            {
                if (DoesArchetypeMatchQuery(archetype, types))
                {
                    yield return archetype;
                }
            }
        }

        private bool DoesArchetypeMatchQuery(Archetype archetype, ComponentType[] queryTypes)
        {
            foreach (var queryType in queryTypes)
            {
                if (!archetype.HasComponent(queryType))
                {
                    return false;
                }
            }
            return true;
        }

        private int CalculateArchetypeHash(ComponentType[] types)
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

            foreach (var hash in emptyArchetypes)
            {
                archetypes.Remove(hash);
            }
        }
    }
}
