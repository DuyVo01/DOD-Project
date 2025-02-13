using System;
using UnityEngine;

namespace EventChannel
{
    public class EventChannelSO<T> : ScriptableObject
    {
        [SerializeField]
        private int maxListeners = 8;
        private Action<T>[] listeners;
        private T lastEventData;
        private bool hasEventOccurred;
        private readonly object lockObject = new object();

        private void InitializeIfNeeded()
        {
            if (listeners == null)
            {
                listeners = new Action<T>[maxListeners];
            }
        }

        public void RaiseEvent(T eventData)
        {
            if (listeners == null)
            {
                return;
            }

            if (eventData == null)
            {
                Debug.LogError($"Attempted to raise null event data in {name}");
                return;
            }

            lock (lockObject)
            {
                lastEventData = eventData;
                hasEventOccurred = true;

                for (int i = 0; i < listeners.Length; i++)
                {
                    if (listeners[i] != null)
                    {
                        try
                        {
                            listeners[i].Invoke(eventData);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error invoking event listener in {name}: {e}");
                        }
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

            lock (lockObject)
            {
                InitializeIfNeeded();

                for (int i = 0; i < listeners.Length; i++)
                {
                    if (listeners[i] == listener)
                    {
                        Debug.LogWarning($"Attempted to subscribe duplicate listener to {name}");
                        return false;
                    }
                }

                for (int i = 0; i < listeners.Length; i++)
                {
                    if (listeners[i] == null)
                    {
                        listeners[i] = listener;

                        if (invokeLastEvent && hasEventOccurred)
                        {
                            try
                            {
                                listener.Invoke(lastEventData);
                            }
                            catch (Exception e)
                            {
                                Debug.LogError($"Error invoking late subscriber in {name}: {e}");
                            }
                        }
                        return true;
                    }
                }

                Debug.LogError(
                    $"Failed to subscribe listener to {name}: Maximum listeners ({maxListeners}) reached"
                );
                return false;
            }
        }

        public void Unsubscribe(Action<T> listener)
        {
            if (listener == null || listeners == null)
                return;

            lock (lockObject)
            {
                for (int i = 0; i < listeners.Length; i++)
                {
                    if (listeners[i] == listener)
                    {
                        listeners[i] = null;
                        return;
                    }
                }
            }
        }
    }
}
