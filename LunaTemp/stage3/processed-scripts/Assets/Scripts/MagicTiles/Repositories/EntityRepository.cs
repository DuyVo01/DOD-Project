using UnityEngine;

public static class EntityRepository
{
    private static IEntityGroup[] entityGroups;

    static EntityRepository()
    {
        entityGroups = new IEntityGroup[(int)EntityType.Count];
    }

    public static void RegisterEGroup<T>(EntityType type, ref T entityGroup)
        where T : struct, IEntityGroup
    {
        entityGroups[(int)type] = new EntityGroupWrapper<T>(entityGroup);
    }

    public static ref T GetEGroup<T>(EntityType type)
        where T : struct, IEntityGroup
    {
        return ref ((EntityGroupWrapper<T>)entityGroups[(int)type]).Entity;
    }

    public static void Clear()
    {
        entityGroups = new IEntityGroup[(int)EntityType.Count];
    }

    private class EntityGroupWrapper<T> : IEntityGroup
        where T : struct
    {
        public T Entity;

        public EntityGroupWrapper(T entity)
        {
            Entity = entity;
        }
    }
}
