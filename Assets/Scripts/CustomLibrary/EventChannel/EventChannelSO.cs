using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventChannel
{
    public class EventChannelSO<T> : ScriptableObject
    {
        [SerializeField, Min(1)]
        private int initialCapacity = 8;

        private IndexedStorage<EventListener<T>> listeners;
        private int nextListenerId = 0;
        private T lastEventData;
        private bool hasEventOccurred;

        private struct EventListener<TData>
        {
            public object Target;
            public Action<object, TData> Action;
        }

        private void OnEnable()
        {
            InitializeIfNeeded();
        }

        private void InitializeIfNeeded()
        {
            if (listeners == null)
            {
                listeners = new IndexedStorage<EventListener<T>>(initialCapacity);
            }
        }

        public void RaiseEvent(T eventData)
        {
            if (listeners == null || listeners.Count == 0)
                return;

            lastEventData = eventData;
            hasEventOccurred = true;

            //Fast iteration using Span
            var listenersSpan = listeners.AsSpan();
            for (int i = 0; i < listenersSpan.Length; i++)
            {
                var listener = listenersSpan[i];
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

        // Allocation-free subscription
        public int Subscribe<TTarget>(
            TTarget target,
            Action<TTarget, T> method,
            bool invokeLastEvent = false
        )
        {
            if (target == null)
            {
                Debug.LogError($"Attempted to subscribe null target to {name}");
                return -1;
            }

            var cachedAction = CachedDelegate<TTarget, T>.GetOrCreate(method);
            return SubscribeInternal(target, cachedAction, invokeLastEvent);
        }

        private int SubscribeInternal(
            object target,
            Action<object, T> action,
            bool invokeLastEvent = false
        )
        {
            InitializeIfNeeded();

            // Check for duplicate subscription
            var listenersSpan = listeners.AsSpan();
            for (int i = 0; i < listenersSpan.Length; i++)
            {
                var listener = listenersSpan[i];
                if (
                    EqualityComparer<object>.Default.Equals(listener.Target, target)
                    && listener.Action == action
                )
                {
                    Debug.LogWarning($"Duplicate subscription in {name}");

                    // Get the ID for this existing listener
                    int existingId = listeners.GetIdByIndex(i);
                    return existingId;
                }
            }

            // Create a new listener
            int listenerId = nextListenerId++;
            var newListener = new EventListener<T> { Target = target, Action = action };

            // Add to storage
            listeners.Add(listenerId, newListener);

            // Invoke last event if requested
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

            return listenerId;
        }

        public void Unsubscribe(int listenerId)
        {
            if (listeners == null)
                return;

            if (listeners.TryGetById(listenerId, out _))
            {
                listeners.Remove(listenerId);
            }
        }

        public void UnsubscribeAll(object target)
        {
            if (listeners == null || listeners.Count == 0)
                return;

            // Find and remove all listeners with matching target
            var ids = listeners.GetAllIds();
            foreach (var id in ids)
            {
                var listener = listeners.GetById(id);
                if (EqualityComparer<object>.Default.Equals(listener.Target, target))
                {
                    listeners.Remove(id);
                }
            }
        }

        // Proper caching mechanism
        private static class CachedDelegate<TTarget, TData>
        {
            private static Dictionary<Action<TTarget, TData>, Action<object, TData>> cachedActions =
                new Dictionary<Action<TTarget, TData>, Action<object, TData>>();

            public static Action<object, TData> GetOrCreate(Action<TTarget, TData> method)
            {
                if (!cachedActions.TryGetValue(method, out var cachedAction))
                {
                    cachedAction = (target, data) => method((TTarget)target, data);
                    cachedActions[method] = cachedAction;
                }
                return cachedAction;
            }
        }
    }
}
