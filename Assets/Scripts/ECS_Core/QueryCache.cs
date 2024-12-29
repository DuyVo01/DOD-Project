using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_Core
{
    // Stores cached data for a specific archetype in a query
    public class QueryArchetypeCache
    {
        public Archetype Archetype { get; }
        public Array[] ComponentArrays { get; private set; }
        public IReadOnlyList<int> Entities => Archetype.Entities;
        public int Count => Archetype.Count;
        private readonly ComponentType[] queryTypes;

        public QueryArchetypeCache(Archetype archetype, ComponentType[] types)
        {
            Archetype = archetype;
            queryTypes = types;
            ComponentArrays = new Array[types.Length];
            UpdateArrays();
        }

        public void UpdateArrays()
        {
            for (int i = 0; i < queryTypes.Length; i++)
            {
                ComponentArrays[i] = Archetype.GetComponentArrayRaw(queryTypes[i]);
            }
        }
    }

    // Main cache for a specific query type combination
    public class QueryCache
    {
        private readonly ComponentType[] queryTypes;
        private readonly List<QueryArchetypeCache> archetypeCaches = new();
        private readonly ArchetypeManager archetypeManager;
        public bool IsDirty { get; set; } = true;

        public QueryCache(ArchetypeManager archetypeManager, ComponentType[] types)
        {
            this.archetypeManager = archetypeManager;
            this.queryTypes = types;
        }

        public IReadOnlyList<QueryArchetypeCache> GetArchetypeCaches()
        {
            if (IsDirty)
            {
                UpdateCache();
            }
            return archetypeCaches;
        }

        private void UpdateCache()
        {
            archetypeCaches.Clear();
            var matchingArchetypes = archetypeManager.GetArchetypesWithComponents(queryTypes);

            foreach (var archetype in matchingArchetypes)
            {
                archetypeCaches.Add(new QueryArchetypeCache(archetype, queryTypes));
            }

            IsDirty = false;
        }
    }

    // Cache manager to store and retrieve query caches
    public class QueryCacheManager
    {
        private readonly Dictionary<int, QueryCache> queryCaches = new();
        private readonly ArchetypeManager archetypeManager;

        public QueryCacheManager(ArchetypeManager archetypeManager)
        {
            this.archetypeManager = archetypeManager;
        }

        public QueryCache GetOrCreateCache(ComponentType[] types)
        {
            int hash = CalculateQueryHash(types);

            if (!queryCaches.TryGetValue(hash, out var cache))
            {
                cache = new QueryCache(archetypeManager, types);
                queryCaches[hash] = cache;
            }

            return cache;
        }

        private int CalculateQueryHash(ComponentType[] types)
        {
            int hash = 17;
            foreach (var type in types.OrderBy(t => t.Id))
            {
                hash = hash * 31 + type.Id;
            }
            return hash;
        }

        public void MarkAllDirty()
        {
            foreach (var cache in queryCaches.Values)
            {
                cache.IsDirty = true;
            }
        }
    }
}
