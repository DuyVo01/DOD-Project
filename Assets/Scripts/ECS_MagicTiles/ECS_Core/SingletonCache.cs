using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS_Core
{
    // Specialized cache just for singletons
    public class SingletonCache
    {
        private struct CachedComponents
        {
            public Array FlagArray;
            public Array[] ComponentArrays;
            public int EntityIndex;
            public int EntityId;
            public Archetype Archetype; // Store reference to archetype for direct access
        }

        private readonly Dictionary<int, CachedComponents> cacheByFlagType = new();
        private readonly World world;
        private bool isDirty;

        public SingletonCache(World world)
        {
            this.world = world;
        }

        public void GetComponents<TFlag, T1>(out TFlag flag, out T1 component1)
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
        {
            var flagType = ComponentType.Of<TFlag>();
            var comp1Type = ComponentType.Of<T1>();

            if (!TryGetCachedComponents(flagType, out var cached))
            {
                cached = FindAndCacheSingleton<TFlag>(new[] { comp1Type });
            }

            // Get both flag and component
            flag = ((TFlag[])cached.FlagArray)[cached.EntityIndex];
            component1 = ((T1[])cached.ComponentArrays[0])[cached.EntityIndex];
        }

        // Same for two components
        public void GetComponents<TFlag, T1, T2>(
            out TFlag flag,
            out T1 component1,
            out T2 component2
        )
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
        {
            var flagType = ComponentType.Of<TFlag>();
            var compTypes = new[] { ComponentType.Of<T1>(), ComponentType.Of<T2>() };

            if (!TryGetCachedComponents(flagType, out var cached))
            {
                cached = FindAndCacheSingleton<TFlag>(compTypes);
            }

            flag = ((TFlag[])cached.FlagArray)[cached.EntityIndex];
            component1 = ((T1[])cached.ComponentArrays[0])[cached.EntityIndex];
            component2 = ((T2[])cached.ComponentArrays[1])[cached.EntityIndex];
        }

        public void GetComponents<TFlag, T1, T2, T3>(
            out T1 component1,
            out T2 component2,
            out T3 component3
        )
            where TFlag : struct, IComponent
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent
        {
            var flagType = ComponentType.Of<TFlag>();
            var compTypes = new[]
            {
                ComponentType.Of<T1>(),
                ComponentType.Of<T2>(),
                ComponentType.Of<T3>(),
            };

            if (!TryGetCachedComponents(flagType, out var cached))
            {
                cached = FindAndCacheSingleton<TFlag>(compTypes);
            }

            component1 = ((T1[])cached.ComponentArrays[0])[cached.EntityIndex];
            component2 = ((T2[])cached.ComponentArrays[1])[cached.EntityIndex];
            component3 = ((T3[])cached.ComponentArrays[2])[cached.EntityIndex];
        }

        private bool TryGetCachedComponents(ComponentType flagType, out CachedComponents cached)
        {
            if (
                !isDirty
                && cacheByFlagType.TryGetValue(flagType.Id, out cached)
                && world.EntityExists(cached.EntityId)
            )
            {
                return true;
            }

            cached = default;
            return false;
        }

        private CachedComponents FindAndCacheSingleton<TFlag>(ComponentType[] componentTypes)
            where TFlag : struct, IComponent
        {
            var flagType = ComponentType.Of<TFlag>();
            var allTypes = new[] { ComponentType.Of<SingletonFlag>(), flagType }
                .Concat(componentTypes)
                .ToArray();

            var matchingArchetypes = world.ArchetypeManager.GetArchetypesWithComponents(allTypes);
            foreach (var archetype in matchingArchetypes)
            {
                var entities = archetype.Entities;
                for (int i = 0; i < archetype.Count; i++)
                {
                    int entityId = entities[i];
                    if (!world.IsMarkedForDestruction(entityId))
                    {
                        // Get flag component array
                        var flagArray = archetype.GetComponentArrayRaw(flagType);

                        // Get data component arrays
                        var arrays = new Array[componentTypes.Length];
                        for (int j = 0; j < componentTypes.Length; j++)
                        {
                            arrays[j] = archetype.GetComponentArrayRaw(componentTypes[j]);
                        }

                        var cached = new CachedComponents
                        {
                            FlagArray = flagArray, // Store flag array
                            ComponentArrays = arrays,
                            EntityIndex = i,
                            EntityId = entityId,
                            Archetype = archetype,
                        };

                        cacheByFlagType[flagType.Id] = cached;
                        return cached;
                    }
                }
            }

            throw new InvalidOperationException(
                $"No singleton found with flag {typeof(TFlag).Name}"
            );
        }

        public void MarkDirty()
        {
            isDirty = true;
        }

        public void Clear()
        {
            cacheByFlagType.Clear();
            isDirty = true;
        }
    }
}
