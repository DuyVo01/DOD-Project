using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEventChannelSO : ScriptableObject
{
    protected readonly HashSet<WeakReference> listeners = new();

    protected void CleanupListeners()
    {
        listeners.RemoveWhere(weakRef => !weakRef.IsAlive);
    }
}
