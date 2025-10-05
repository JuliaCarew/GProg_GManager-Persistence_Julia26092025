using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Configuration")]
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int level1SceneIndex = 1;
    [SerializeField] private int level2SceneIndex = 2;
    [SerializeField] private int level3SceneIndex = 3;

    /// <summary>
    /// Loads a scene by index
    /// </summary>
    /// <param name="sceneIndex">
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Loading scene {sceneIndex}");
            SceneManager.LoadScene(sceneIndex);
        }
        else
            Debug.LogWarning($"Invalid scene index: {sceneIndex}");
    }

    #region Load Scene Methods (Main menu - lv 3)
    public void LoadMainMenu() => LoadScene(mainMenuSceneIndex);
        
    public void LoadLevel1() => LoadScene(level1SceneIndex);

    public void LoadLevel2() => LoadScene(level2SceneIndex);

    public void LoadLevel3() => LoadScene(level3SceneIndex);

    #endregion

    public void LoadNewGame()
    {
        // Reset player data 
        if (PlayerData.Instance != null)
        {
            PlayerData.Instance.health = 100;
            PlayerData.Instance.xp = 0;
            PlayerData.Instance.score = 0;
            PlayerData.Instance.coins = 0;
            PlayerData.Instance.level = 1;
            PlayerData.Instance.mana = 50;
        }

        LoadLevel1();
    }
    
    public int GetCurrentSceneIndex() { return SceneManager.GetActiveScene().buildIndex; } 
    public string GetCurrentSceneName() {return SceneManager.GetActiveScene().name;}
}
