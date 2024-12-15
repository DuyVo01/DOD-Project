using UnityEngine;

// Bridge repository to manage presenter systems
public static class BridgeRepository
{
    private static IBridge[] _bridges;

    static BridgeRepository()
    {
        _bridges = new IBridge[(int)BridgeType.Count];
    }

    public static void RegisterBridge<T>(BridgeType type, T bridge)
        where T : struct, IBridge
    {
        _bridges[(int)type] = new BridgeWrapper<T>(bridge);
    }

    public static ref T GetBridge<T>(BridgeType type)
        where T : struct, IBridge
    {
        return ref ((BridgeWrapper<T>)_bridges[(int)type]).Data;
    }

    private class BridgeWrapper<T> : IBridge
        where T : struct
    {
        public T Data;

        public BridgeWrapper(T data) => Data = data;
    }
}
