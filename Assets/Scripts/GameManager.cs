using UnityEngine;
using System.IO;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{

    #region Singleton Testing / Awake
    // set up singleton testing first
    public static int GameManagerCount;
    
    // enable testing mode (disable singleton)
    public static void EnableTestingMode()
    {
        SetSingletonEnabled(false);
        GameManagerCount = 0; 
        Debug.Log("Testing mode enabled - multiple GameManager instances allowed");
    }
    
    // disable testing mode (enable singleton)
    public static void DisableTestingMode()
    {
        SetSingletonEnabled(true);
        GameManagerCount = 1; 
        Debug.Log("Testing mode disabled - singleton behavior restored");
    }

    // scene loading
    public int sceneToLoad;
    private SceneLoader sceneLoader;

    protected override void Awake()
    {
        base.Awake();
        
        // update count based on singleton state
        if (IsSingletonEnabled())
        {
            GameManagerCount = 1;
            Debug.Log($"GameManager instantiated (Singleton Mode). Count: {GameManagerCount}");
        }
        else
        {
            GameManagerCount++;
            Debug.Log($"GameManager instantiated (Testing Mode). Count: {GameManagerCount}");
        }
        
        sceneLoader = GetComponent<SceneLoader>();
        if (sceneLoader == null)
            sceneLoader = gameObject.AddComponent<SceneLoader>();
        
    }
    #endregion

    #region Scene Loading Input
    void Update() => HandleKeyboardInput();

    private void HandleKeyboardInput()
    {
        if (sceneLoader == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            sceneLoader.LoadScene(0); // Main Menu

        if (Input.GetKeyDown(KeyCode.Alpha2))
            sceneLoader.LoadScene(1); // Level 1

        if (Input.GetKeyDown(KeyCode.Alpha3))
            sceneLoader.LoadScene(2); // Level 2

        if (Input.GetKeyDown(KeyCode.Alpha4))
            sceneLoader.LoadScene(3); // Level 3

    }

    #endregion

    #region GUI Buttons
    void OnGUI()
    {
        if (sceneLoader == null) return;
        
        // current scene information
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 120, 200, 30), "Current Scene: " + sceneLoader.GetCurrentSceneName());

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 100, 200, 30), "Scene Index: " + sceneLoader.GetCurrentSceneIndex());
        
        // keyboard controls
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 80, 200, 30), "Press 1-4 to switch scenes");

        // scene loading buttons
        if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 50, 100, 40), "Main Menu"))
            sceneLoader.LoadMainMenu();

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 40), "Level 1"))
            sceneLoader.LoadLevel1();

        if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height - 50, 100, 40), "Level 2"))
            sceneLoader.LoadLevel2();

        if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height - 50, 100, 40), "Level 3"))
            sceneLoader.LoadLevel3();
    }
    #endregion

    #region Save / Load

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerInfo info = new PlayerInfo();
        info.health = PlayerData.Instance.health;
        info.experience = PlayerData.Instance.xp;
        info.score = PlayerData.Instance.score;
        info.coins = PlayerData.Instance.coins;
        info.level = PlayerData.Instance.level;
        info.mana = PlayerData.Instance.mana;

        bf.Serialize(file, info);
        file.Close();

        Debug.Log("Game saved with all player data!");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerInfo info = (PlayerInfo)bf.Deserialize(file);
            file.Close();

            PlayerData.Instance.health = (int)info.health;
            PlayerData.Instance.xp = (int)info.experience;
            PlayerData.Instance.score = (int)info.score;
            PlayerData.Instance.coins = (int)info.coins;
            PlayerData.Instance.level = (int)info.level;
            PlayerData.Instance.mana = (int)info.mana;

            Debug.Log("Game loaded with all player data!");
        }
        else
            Debug.Log("No save file found!");
    }
    
    #endregion
}

[Serializable]
class PlayerInfo
{
    public float health;
    public float experience;
    public float score;
    public float coins;
    public float level;
    public float mana;
}