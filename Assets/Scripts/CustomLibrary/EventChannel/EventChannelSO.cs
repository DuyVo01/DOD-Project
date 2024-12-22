using System;
using System.Collections.Generic;
using UnityEngine;

public class EventChannelSO<T> : BaseEventChannelSO
    where T : IEventData
{
    private readonly List<Action<T>> onEventRaised = new();

    public void RaiseEvent(T eventData)
    {
        CleanupListeners();

        for (int i = onEventRaised.Count - 1; i >= 0; i--)
        {
            try
            {
                onEventRaised[i]?.Invoke(eventData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error invoking event listener: {e}");
            }
        }
    }

    public void Subscribe(Action<T> listener)
    {
        if (!onEventRaised.Contains(listener))
        {
            onEventRaised.Add(listener);
            listeners.Add(new WeakReference(listener.Target));
        }
    }

    public void Unsubscribe(Action<T> listener)
    {
        onEventRaised.Remove(listener);
        CleanupListeners();
    }
}
