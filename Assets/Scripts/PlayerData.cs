using UnityEngine;

public class PlayerData : SingletonBase<PlayerData>
{
    #region Player Stat Variables
    // Player/game state
    public int health = 100;
    public int xp = 0;
    public int score = 0;

    // other data variables
    public int coins = 0;
    public int level = 1;
    public int mana = 50;
    
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 30), "Health: " + health);
        GUI.Label(new Rect(10, 40, 150, 30), "Experience: " + xp);
        GUI.Label(new Rect(10, 70, 150, 30), "Score: " + score);
        GUI.Label(new Rect(10, 100, 150, 30), "Coins: " + coins);
        GUI.Label(new Rect(10, 130, 150, 30), "Level: " + level);
        GUI.Label(new Rect(10, 160, 150, 30), "Mana: " + mana);
    }
}
