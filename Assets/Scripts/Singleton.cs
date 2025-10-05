using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _singletonEnabled = true; // default - enabled
    
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<T>();
            return _instance;
        }
    }
    
    // Method to enable/disable singleton behavior
    public static void SetSingletonEnabled(bool enabled)
    {
        _singletonEnabled = enabled;
        Debug.Log($"Singleton behavior {(enabled ? "enabled" : "disabled")} for {typeof(T).Name}");
    }
    
    public static bool IsSingletonEnabled() {return _singletonEnabled;}

    protected virtual void Awake()
    {
        // If singleton is disabled, skip singleton behavior
        if (!_singletonEnabled)
        {
            Debug.Log($"{typeof(T).Name} instantiated (Singleton disabled)");
            return;
        }
        
        // Normal singleton behavior
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}