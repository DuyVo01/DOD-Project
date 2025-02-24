using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PresenterManagerRepository
{
    private static IPresenterManager[] _managers;

    static PresenterManagerRepository()
    {
        _managers = new IPresenterManager[(int)PresenterManagerType.Count];
    }

    public static void RegisterManager<T>(PresenterManagerType type, T manager)
        where T : struct, IPresenterManager
    {
        _managers[(int)type] = new ManagerWrapper<T>(manager);
    }

    public static ref T GetManager<T>(PresenterManagerType type)
        where T : struct, IPresenterManager
    {
        return ref ((ManagerWrapper<T>)_managers[(int)type]).Data;
    }

    private class ManagerWrapper<T> : IPresenterManager
        where T : struct
    {
        public T Data;

        public ManagerWrapper(T data) => Data = data;
    }
}
