using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>
/// Persistent Game Manager handling state & scene switching.
/// </summary>
public class GameManager : SingletonBase<GameManager>
{
    // Debug: how many GameManagers exist
    public static int GameManagerCount = 0;

    // Player/game state
    public int health = 100;
    public int xp = 0;
    public int score = 0;

    // extra variables 
    public int coins = 0;
    public int level = 1;
    public int mana = 50;

    private string saveFilePath;

    protected override void Awake()
    {
        base.Awake();
        GameManagerCount++;

        // set up save data path
        saveFilePath = Path.Combine(Application.persistentDataPath, "save.json");
        
        Debug.Log($"GameManager instantiated. Count: {GameManagerCount}");
    }

    private void Update()
    {
        // quick scene switch for testing
        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene(3);

        // quick save/load test
        if (Input.GetKeyDown(KeyCode.F5)) SaveGame();
        if (Input.GetKeyDown(KeyCode.F9)) LoadGame();
    }

    #region Save / Load

    [System.Serializable]
    public class SaveData
    {
        public int health;
        public int xp;
        public int score;
        public int coins;
        public int level;
        public int mana;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData()
        {
            health = health,
            xp = xp,
            score = score,
            coins = coins,
            level = level,
            mana = mana
        };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            health = data.health;
            xp = data.xp;
            score = data.score;
            coins = data.coins;
            level = data.level;
            mana = data.mana;

            Debug.Log("Game loaded from " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }

    #endregion
}
