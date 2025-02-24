using System;
using UnityEngine;

public class ManualDebug : MonoBehaviour
{
    private static ManualDebug _instance;
    private static bool _isTriggered;

    [SerializeField]
    private KeyCode triggerKey = KeyCode.F9;

    [SerializeField]
    private bool enableDebugging = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!enableDebugging)
            return;
        _isTriggered = Input.GetKeyDown(triggerKey);
    }

    // Simple factory method for creating debug handlers
    public static T CreateLog<T>(string message, params object[] args)
        where T : BaseDebugHandler, new()
    {
        if (_isTriggered)
        {
            var handler = new T();
            handler.Initialize(message, args);
            return handler;
        }

        return null;
    }
}
