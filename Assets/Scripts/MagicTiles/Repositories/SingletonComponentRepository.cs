public static class SingletonComponentRepository
{
    private static IDataComponent[] _singletonComponents;

    static SingletonComponentRepository()
    {
        _singletonComponents = new IDataComponent[(int)SingletonComponentType.Count];
    }

    public static void RegisterComponent<T>(SingletonComponentType type, T component)
        where T : struct, IDataComponent
    {
        _singletonComponents[(int)type] = new SingletonComponentWrapper<T>(component);
    }

    public static ref T GetComponent<T>(SingletonComponentType type)
        where T : struct, IDataComponent
    {
        return ref ((SingletonComponentWrapper<T>)_singletonComponents[(int)type]).Data;
    }

    private class SingletonComponentWrapper<T> : IDataComponent
        where T : struct
    {
        public T Data;

        public SingletonComponentWrapper(T data) => Data = data;
    }
}

public enum SingletonComponentType
{
    PerfectLine,
    MusicNotePresenterManager,
    Count,
}
