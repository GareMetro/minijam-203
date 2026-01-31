using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private new void Awake()
    {
        base.Awake();
        
    }

    private void Start() {
        FoodManager.Instance.Initialize();
    }

    public void Victory()
    {
        // stop game
        
        
        // temp solution
        Debug.Log("Victory !!!1!1!11!!");
        Time.timeScale = 0;
        
        
        // display victory UI
    }

    public void Defeat()
    {
        // stop game
        // display defeat UI
    }
}
