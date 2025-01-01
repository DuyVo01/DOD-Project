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
        private readonly int[] componentIndices;

        public QueryArchetypeCache(Archetype archetype, ComponentType[] types)
        {
            Archetype = archetype;
            queryTypes = types;
            componentIndices = new int[types.Length];
            ComponentArrays = new Array[types.Length];
            UpdateArrays();

            // Precalculate component indices for faster access
            for (int i = 0; i < types.Length; i++)
            {
                componentIndices[i] = Array.IndexOf(archetype.ComponentTypes.ToArray(), types[i]);
            }
        }

        public void UpdateArrays()
        {
            for (int i = 0; i < queryTypes.Length; i++)
            {
                ComponentArrays[i] = Archetype.GetComponentArrayRaw(queryTypes[i]);
            }
        }

        public ReadOnlySpan<T> GetComponents<T>()
            where T : struct, IComponent
        {
            var type = ComponentType.Of<T>();
            int index = Array.IndexOf(queryTypes, type);
            if (index < 0)
                throw new ArgumentException($"Type {typeof(T)} not in query");

            return Archetype.GetComponentArray<T>();
        }

        public ReadOnlySpan<int> GetEntities() => Archetype.GetEntities();
    }

    // Main cache for a specific query type combination
    public class QueryCache
    {
        private readonly ComponentType[] queryTypes;
        private readonly List<QueryArchetypeCache> archetypeCaches = new();
        private readonly World world;
        private QueryVersionInfo versionInfo;
        public bool IsDirty { get; set; } = true;
        public uint lastProcessedVersion
        {
            get => versionInfo.lastProcessedVersion;
            set => versionInfo.lastProcessedVersion = value;
        }

        public ComponentType[] QueryTypes => queryTypes;

        public QueryCache(World world, ComponentType[] types)
        {
            this.world = world;
            this.queryTypes = types;
        }

        public IReadOnlyList<QueryArchetypeCache> GetArchetypeCaches()
        {
            var worldVersion = world.GetStructuralVersion();
            if (!versionInfo.IsValid(worldVersion))
            {
                UpdateCache(worldVersion);
            }

            return archetypeCaches;
        }

        private void UpdateCache(in StructuralChangeVersion worldVersion)
        {
            archetypeCaches.Clear();
            var matchingArchetypes = world.ArchetypeManager.GetArchetypesWithComponents(queryTypes);

            foreach (var archetype in matchingArchetypes)
            {
                if (archetype.Count > 0)
                {
                    archetypeCaches.Add(new QueryArchetypeCache(archetype, queryTypes));
                }
            }

            // Update version info
            versionInfo.lastProcessedVersion = worldVersion.GlobalVersion;
            versionInfo.lastUpdatedFrame = worldVersion.FrameNumber;
        }
    }

    // Cache manager to store and retrieve query caches
    public class QueryCacheManager
    {
        private readonly Dictionary<int, QueryCache> queryCaches = new();
        private readonly ArchetypeManager archetypeManager;
        private readonly World world;

        public QueryCacheManager(World world, ArchetypeManager archetypeManager)
        {
            this.world = world;
            this.archetypeManager = archetypeManager;
        }

        public QueryCache GetOrCreateCache(ComponentType[] types)
        {
            int hash = CalculateQueryHash(types);

            if (!queryCaches.TryGetValue(hash, out var cache))
            {
                cache = new QueryCache(world, types);
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

    // Helper extension for QueryCache
    public static class QueryCacheExtensions
    {
        public static void MarkDirty(this QueryCache cache)
        {
            // Forces the cache to update on next use
            cache.lastProcessedVersion = 0;
        }

        public static bool UsesComponent(this QueryCache cache, ComponentType type)
        {
            return cache.QueryTypes.Contains(type);
        }
    }
}
