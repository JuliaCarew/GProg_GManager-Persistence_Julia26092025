using UnityEngine;

public class SingletonTestScript : MonoBehaviour
{
    [Header("Singleton Testing")]
    [SerializeField] private bool enableSingleton = true;
    [SerializeField] private bool createMultipleManagers = false;
    
    private GameObject gameManagerPrefab;
    
    void Start()
    {
        // create GameManager prefab for testing
        CreateGameManagerPrefab();
        
        if (createMultipleManagers)
            TestMultipleInstances();
        
    }

    #region GUI Buttons
    void OnGUI()
    {
        GUI.Label(new Rect(10, 400, 300, 30), $"GameManager Count: {GameManager.GameManagerCount}");

        // current testing mode status
        string singletonStatus = GameManager.IsSingletonEnabled() ? "Singleton: ENABLED" : "Singleton: DISABLED";
        GUI.Label(new Rect(170, 400, 300, 30), singletonStatus);

        // enable testing
        if (GUI.Button(new Rect(10, 440, 150, 30), "Enable Testing"))
            GameManager.EnableTestingMode();
        
        // disable testing
        if (GUI.Button(new Rect(170, 440, 150, 30), "Disable Testing"))
            GameManager.DisableTestingMode();

        if (GUI.Button(new Rect(10, 480, 150, 30), "Test Multiple Instances"))
            TestMultipleInstances();

        if (GUI.Button(new Rect(170, 480, 150, 30), "Clear All GameManagers"))
            ClearAllGameManagers();

        if (GUI.Button(new Rect(10, 520, 150, 30), "Reset Count"))
            GameManager.GameManagerCount = 0;
        
        if (GUI.Button(new Rect(170, 520, 150, 30), "Recreate Prefab"))
        {
            if (gameManagerPrefab != null)
                DestroyImmediate(gameManagerPrefab);
            
            CreateGameManagerPrefab();
        }
    }
    
    #endregion
    
    private void CreateGameManagerPrefab()
    {
        // create GameObject with GameManager and SceneLoader
        GameObject managerObj = new GameObject("GameManager");
        managerObj.AddComponent<GameManager>();
        managerObj.AddComponent<SceneLoader>();
        managerObj.AddComponent<PlayerData>();

        managerObj.SetActive(false); // deactivate to use as prefab
        gameManagerPrefab = managerObj;

        Debug.Log("GameManager prefab created");
    }

    private void TestMultipleInstances()
    {
        Debug.Log("Testing multiple GameManager instances...");
        
        if (gameManagerPrefab == null)
        {
            Debug.LogWarning("GameManager prefab is null, creating...");
            CreateGameManagerPrefab();
        }
        
        // verify prefab is still valid before instantiating
        if (gameManagerPrefab != null)
        {
            // create multiple GameManager instances
            for (int i = 0; i < 3; i++)
            {
                GameObject newManager = Instantiate(gameManagerPrefab);
                newManager.name = $"GameManager_Test_{i}";
            }
            
            Debug.Log($"Created multiple instances. Current count: {GameManager.GameManagerCount}");
        }
        else
            Debug.LogError("Could not create GameManager prefab for testing");
    }
    
    private void ClearAllGameManagers()
    {
        // find and destroy all GameManager objects
        GameManager[] managers = FindObjectsOfType<GameManager>();
        foreach (GameManager manager in managers)
        {
            if (manager.gameObject != null)
            {
                DestroyImmediate(manager.gameObject);
            }
        }
        
        // reset the prefab reference
        if (gameManagerPrefab != null)
        {
            DestroyImmediate(gameManagerPrefab);
            gameManagerPrefab = null;
        }
        
        GameManager.GameManagerCount = 0;
        
        Debug.Log("Cleared all GameManager instances and reset prefab");
    }
}
