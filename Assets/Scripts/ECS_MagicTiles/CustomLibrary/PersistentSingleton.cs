using UnityEngine;

public abstract class PersistentSingleton<T> : MonoBehaviour
    where T : Component
{
    private static volatile T _instance;
    private static bool _initialized;
    private static readonly string TypeName = typeof(T).Name;

    public static T Instance
    {
        get
        {
            if (!_initialized)
            {
                InitializeInstance();
                _initialized = true;
            }
            return _instance;
        }
    }

    private static void InitializeInstance()
    {
        if (_instance != null)
            return;

        _instance = FindObjectOfType<T>();
        if (_instance == null)
        {
            var go = new GameObject($"[{TypeName}]");
            _instance = go.AddComponent<T>();
        }
        DontDestroyOnLoad(_instance.gameObject);
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this as T;
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }

    protected virtual void OnDestroy() { }
}
