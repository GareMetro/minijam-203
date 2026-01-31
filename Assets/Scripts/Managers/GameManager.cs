using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EndGameUI endGameUI;
    private new void Awake()
    {
        base.Awake();
        if (!endGameUI)
        {
            Debug.LogError("GameManager is missing end game UI");
            return;
        } 
        endGameUI.gameObject.SetActive(false);
    }

    private void Start() {
        FoodManager.Instance.Initialize();
    }

    public void Victory()
    {
        // stop game
        
        
        // temp solution
        Time.timeScale = 0;
        
        // display victory UI
        if (!endGameUI)
        {
            Debug.LogError("GameManager is missing end game UI");
            return;
        }
        
        endGameUI.gameObject.SetActive(true);
        endGameUI.DisplayEndGame(true);
    }

    public void Defeat()
    {
        // stop game
        
        // temp solution
        Time.timeScale = 0;
        
        if (!endGameUI)
        {
            Debug.LogError("GameManager is missing end game UI");
            return;
        }
        
        // display defeat UI
        endGameUI.gameObject.SetActive(true);
        endGameUI.DisplayEndGame(false);
    }
}
