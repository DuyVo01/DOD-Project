using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventChannel
{
    public class EventChannelSO<T> : ScriptableObject
    {
        [SerializeField]
        private int maxListeners = 8;

        private Listener<T>[] listeners;
        private T lastEventData;
        private bool hasEventOccurred;

        private struct Listener<TData> 
        {
            public object Target;
            public Action<object, TData> Action;
        }

        private void InitializeIfNeeded()
        {
            if (listeners == null)
            {
                listeners = new Listener<T>[maxListeners];
            }
        }

        public void RaiseEvent(T eventData)
        {
            if (listeners == null)
                return;

            lastEventData = eventData;
            hasEventOccurred = true;

            for (int i = 0; i < listeners.Length; i++)
            {
                var listener = listeners[i];
                if (listener.Action != null)
                {
                    try
                    {
                        listener.Action(listener.Target, eventData);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error invoking listener in {name}: {e}");
                    }
                }
            }
        }

        public bool Subscribe(Action<T> listener, bool invokeLastEvent = false)
        {
            if (listener == null)
            {
                Debug.LogError($"Attempted to subscribe null listener to {name}");
                return false;
            }

            // Wrap in closure-based delegate (allocates)
            return SubscribeInternal(listener.Target, (_, data) => listener(data), invokeLastEvent);
        }

        // Allocation-free subscription
        public bool Subscribe<TTarget>(
            TTarget target,
            Action<TTarget, T> method,
            bool invokeLastEvent = false
        )
        {
            if (target == null)
            {
                Debug.LogError($"Attempted to subscribe null target to {name}");
                return false;
            }

            var cachedAction = CachedDelegate<TTarget, T>.GetOrCreate(method);
            return SubscribeInternal(target, cachedAction, invokeLastEvent);
        }

        private bool SubscribeInternal(
            object target,
            Action<object, T> action,
            bool invokeLastEvent = false
        )
        {
            InitializeIfNeeded();
            for (int i = 0; i < listeners.Length; i++)
            {
                if (listeners[i].Target == target && listeners[i].Action == action)
                {
                    Debug.LogWarning($"Duplicate subscription in {name}");
                    return false;
                }
            }

            // Find empty slot
            for (int i = 0; i < listeners.Length; i++)
            {
                if (listeners[i].Action == null)
                {
                    listeners[i] = new Listener<T> { Target = target, Action = action };

                    if (invokeLastEvent && hasEventOccurred)
                    {
                        try
                        {
                            action(target, lastEventData);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error invoking late subscriber in {name}: {e}");
                        }
                    }
                    return true;
                }
            }

            Debug.LogError($"Max listeners ({maxListeners}) reached in {name}");
            return false;
        }

        public void Unsubscribe<TTarget>(TTarget target, Action<TTarget, T> method)
        {
            if (listeners == null)
                return;

            var cachedAction = CachedDelegate<TTarget, T>.GetOrCreate(method);
            for (int i = 0; i < listeners.Length; i++)
            {
                if (
                    EqualityComparer<object>.Default.Equals(listeners[i].Target, target)
                    && listeners[i].Action == cachedAction
                )
                {
                    listeners[i] = default;
                    return;
                }
            }
        }

        // Caching mechanism
        private static class CachedDelegate<TTarget, TData>
        {
            private static Action<object, TData> cachedAction;

            public static Action<object, TData> GetOrCreate(Action<TTarget, TData> method)
            {
                if (cachedAction == null)
                {
                    cachedAction = (target, data) => method((TTarget)target, data);
                }
                return cachedAction;
            }
        }
    }
}
