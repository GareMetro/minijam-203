using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EndGameUI endGameUI;

#if UNITY_EDITOR
    [SerializeField] private bool DontEndGame = false;
#endif

    bool isPause = false;

    [SerializeField] public GameObject PauseMenuPrefab;
    private GameObject PauseMenu;

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
#if UNITY_EDITOR
        if (DontEndGame) return;
#endif

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
#if UNITY_EDITOR
        if (DontEndGame) return;
#endif

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

    public void HideOrShowPauseMenu()
    {
        if(isPause)
        {
            isPause = !isPause;
            if(PauseMenu!=null)
            {
                Destroy(PauseMenu);
            }
            Time.timeScale = 1;

        }
        else
        {
            isPause = !isPause;
            PauseMenu = Instantiate(PauseMenuPrefab);
            Time.timeScale = 0;

        }
    }
}
