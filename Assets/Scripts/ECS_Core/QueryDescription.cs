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

        public void ForEach(ArchetypeManager archetypeManager, ActionRef<int, T1> action)
        {
            var matchingArchetypes = archetypeManager.GetArchetypesWithComponents(componentTypes);
            foreach (var archetype in matchingArchetypes)
            {
                var component1Array = archetype.GetComponentArray<T1>();
                var entities = archetype.Entities;

                for (int i = 0; i < archetype.Count; i++)
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

        public void ForEach(ArchetypeManager archetypeManager, ActionRef<int, T1, T2> action)
        {
            var matchingArchetypes = archetypeManager.GetArchetypesWithComponents(componentTypes);
            foreach (var archetype in matchingArchetypes)
            {
                var component1Array = archetype.GetComponentArray<T1>();
                var component2Array = archetype.GetComponentArray<T2>();
                var entities = archetype.Entities;

                for (int i = 0; i < archetype.Count; i++)
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

        public void ForEach(ArchetypeManager archetypeManager, ActionRef<int, T1, T2, T3> action)
        {
            var matchingArchetypes = archetypeManager.GetArchetypesWithComponents(componentTypes);
            foreach (var archetype in matchingArchetypes)
            {
                var component1Array = archetype.GetComponentArray<T1>();
                var component2Array = archetype.GetComponentArray<T2>();
                var component3Array = archetype.GetComponentArray<T3>();
                var entities = archetype.Entities;

                for (int i = 0; i < archetype.Count; i++)
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

        public void ForEach(
            ArchetypeManager archetypeManager,
            ActionRef<int, T1, T2, T3, T4> action
        )
        {
            var matchingArchetypes = archetypeManager.GetArchetypesWithComponents(componentTypes);
            foreach (var archetype in matchingArchetypes)
            {
                var component1Array = archetype.GetComponentArray<T1>();
                var component2Array = archetype.GetComponentArray<T2>();
                var component3Array = archetype.GetComponentArray<T3>();
                var component4Array = archetype.GetComponentArray<T4>();
                var entities = archetype.Entities;

                for (int i = 0; i < archetype.Count; i++)
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
