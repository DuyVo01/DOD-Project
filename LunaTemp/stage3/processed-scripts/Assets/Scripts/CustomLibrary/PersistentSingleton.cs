using UnityEngine;

public abstract class PersistentSingleton<T> : MonoBehaviour
    where T : Component
{
    // Static instance reference - readonly for thread safety
    private static readonly object _lock = new object();
    private static T _instance;

    // Public getter with lazy initialization and thread safety
    public static T Instance
    {
        get
        {
            // For optimization - avoid lock if already initialized
            if (_instance != null)
                return _instance;

            lock (_lock)
            {
                // Find if there's an instance in the scene
                _instance = FindAnyObjectByType<T>();

                // Create new instance if none exists
                if (_instance == null)
                {
                    var go = new GameObject($"[{typeof(T).Name}]");
                    _instance = go.AddComponent<T>();
                }

                // Make persistent
                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        // If an instance already exists and it's not this one
        if (_instance != null && _instance != this)
        {
            // Destroy this duplicate
            Destroy(gameObject);
            return;
        }

        // Set up singleton instance
        _instance = this as T;
        DontDestroyOnLoad(gameObject);

        OnAwake();
    }

    // Optional override for child classes
    protected virtual void OnAwake() { }
}
