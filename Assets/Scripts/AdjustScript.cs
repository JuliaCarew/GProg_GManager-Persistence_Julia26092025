using UnityEngine;

public class AdjustScript : MonoBehaviour
{
    void OnGUI()
    {
        // health
        if (GUI.Button(new Rect(10, 200, 100, 30), "Health up"))
            PlayerData.Instance.health += 10;
        
        if (GUI.Button(new Rect(10, 240, 100, 30), "Health down"))
            PlayerData.Instance.health -= 10;
            
        
        // experience
        if (GUI.Button(new Rect(10, 280, 100, 30), "XP up"))
            PlayerData.Instance.xp += 10;
        
        if (GUI.Button(new Rect(10, 320, 100, 30), "XP down"))
            PlayerData.Instance.xp -= 10;
        

        // score
        if (GUI.Button(new Rect(120, 200, 100, 30), "Score up"))
            PlayerData.Instance.score += 10;
        
        if (GUI.Button(new Rect(120, 240, 100, 30), "Score down"))
            PlayerData.Instance.score -= 10;
        

        // coins
        if (GUI.Button(new Rect(120, 280, 100, 30), "Coins up"))
            PlayerData.Instance.coins += 5;
        
        if (GUI.Button(new Rect(120, 320, 100, 30), "Coins down"))
            PlayerData.Instance.coins -= 5;
        

        // level
        if (GUI.Button(new Rect(230, 200, 100, 30), "Level up"))
            PlayerData.Instance.level += 1;
        
        if (GUI.Button(new Rect(230, 240, 100, 30), "Level down"))
            PlayerData.Instance.level -= 1;
        

        // mana
        if (GUI.Button(new Rect(230, 280, 100, 30), "Mana up"))
            PlayerData.Instance.mana += 10;
        
        if (GUI.Button(new Rect(230, 320, 100, 30), "Mana down"))
            PlayerData.Instance.mana -= 10;
        

        // save/load
        if (GUI.Button(new Rect(10, 360, 100, 30), "Save"))
            GameManager.Instance.Save();
        
        if (GUI.Button(new Rect(120, 360, 100, 30), "Load"))
            GameManager.Instance.Load();
        
        if (GUI.Button(new Rect(230, 360, 100, 30), "New Game"))
            GameManager.Instance.GetComponent<SceneLoader>().LoadNewGame();
    }
}
