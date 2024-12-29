namespace ECS_Core
{
    public delegate void ActionRef<T1>(ref T1 item);
    public delegate void ActionRef<T1, T2>(T1 index, ref T2 item);
    public delegate void ActionRef<T1, T2, T3>(T1 index, ref T2 item1, ref T3 item2);
    public delegate void ActionRef<T1, T2, T3, T4>(
        T1 index,
        ref T2 item1,
        ref T3 item2,
        ref T4 item3
    );
    public delegate void ActionRef<T1, T2, T3, T4, T5>(
        T1 index,
        ref T2 item1,
        ref T3 item2,
        ref T4 item3,
        ref T5 item4
    );

    public readonly struct QueryDescription<T1>
        where T1 : struct, IComponent
    {
        private static readonly ComponentType[] componentTypes = new[] { ComponentType.Of<T1>() };

        public void ForEach(World world, ActionRef<int, T1> action)
        {
            var cache = world.GetQueryCache(componentTypes);
            var archetypeCaches = cache.GetArchetypeCaches();

            foreach (var archetypeCache in archetypeCaches)
            {
                var component1Array = (T1[])archetypeCache.ComponentArrays[0];
                var entities = archetypeCache.Entities;

                for (int i = 0; i < archetypeCache.Count; i++)
                {
                    action(entities[i], ref component1Array[i]);
                }
            }
        }
    }

    public readonly struct QueryDescription<T1, T2>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
    {
        private static readonly ComponentType[] componentTypes = new[]
        {
            ComponentType.Of<T1>(),
            ComponentType.Of<T2>(),
        };

        public void ForEach(World world, ActionRef<int, T1, T2> action)
        {
            var cache = world.GetQueryCache(componentTypes);
            var archetypeCaches = cache.GetArchetypeCaches();

            foreach (var archetypeCache in archetypeCaches)
            {
                var component1Array = (T1[])archetypeCache.ComponentArrays[0];
                var component2Array = (T2[])archetypeCache.ComponentArrays[1];
                var entities = archetypeCache.Entities;

                for (int i = 0; i < archetypeCache.Count; i++)
                {
                    action(entities[i], ref component1Array[i], ref component2Array[i]);
                }
            }
        }
    }

    public readonly struct QueryDescription<T1, T2, T3>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
    {
        private static readonly ComponentType[] componentTypes = new[]
        {
            ComponentType.Of<T1>(),
            ComponentType.Of<T2>(),
            ComponentType.Of<T3>(),
        };

        public void ForEach(World world, ActionRef<int, T1, T2, T3> action)
        {
            var cache = world.GetQueryCache(componentTypes);
            var archetypeCaches = cache.GetArchetypeCaches();

            foreach (var archetypeCache in archetypeCaches)
            {
                var component1Array = (T1[])archetypeCache.ComponentArrays[0];
                var component2Array = (T2[])archetypeCache.ComponentArrays[1];
                var component3Array = (T3[])archetypeCache.ComponentArrays[2];
                var entities = archetypeCache.Entities;

                for (int i = 0; i < archetypeCache.Count; i++)
                {
                    action(
                        entities[i],
                        ref component1Array[i],
                        ref component2Array[i],
                        ref component3Array[i]
                    );
                }
            }
        }
    }

    public readonly struct QueryDescription<T1, T2, T3, T4>
        where T1 : struct, IComponent
        where T2 : struct, IComponent
        where T3 : struct, IComponent
        where T4 : struct, IComponent
    {
        private static readonly ComponentType[] componentTypes = new[]
        {
            ComponentType.Of<T1>(),
            ComponentType.Of<T2>(),
            ComponentType.Of<T3>(),
            ComponentType.Of<T4>(),
        };

        public void ForEach(World world, ActionRef<int, T1, T2, T3, T4> action)
        {
            var cache = world.GetQueryCache(componentTypes);
            var archetypeCaches = cache.GetArchetypeCaches();

            foreach (var archetypeCache in archetypeCaches)
            {
                var component1Array = (T1[])archetypeCache.ComponentArrays[0];
                var component2Array = (T2[])archetypeCache.ComponentArrays[1];
                var component3Array = (T3[])archetypeCache.ComponentArrays[2];
                var component4Array = (T4[])archetypeCache.ComponentArrays[3];
                var entities = archetypeCache.Entities;

                for (int i = 0; i < archetypeCache.Count; i++)
                {
                    action(
                        entities[i],
                        ref component1Array[i],
                        ref component2Array[i],
                        ref component3Array[i],
                        ref component4Array[i]
                    );
                }
            }
        }
    }
}
