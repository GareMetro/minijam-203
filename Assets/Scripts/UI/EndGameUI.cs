using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class EndGameUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI resultTextContainer;
    [SerializeField] public TextMeshProUGUI defeatPhraseContainer;
    public GameObject NextLevelObject;

    [SerializeField] private List<String> defeatPhrases = new();


    private void Start()
    {
        if (NextLevelObject != null)
        {
            if (SceneManager.sceneCountInBuildSettings <= SceneManager.GetActiveScene().buildIndex + 1)
            {
                Destroy(NextLevelObject);
            }
        }
    }

    public void DisplayEndGame(bool victory)
    {
        
        resultTextContainer.text = victory ? "Victory" : "Defeat";
        
        defeatPhraseContainer.text = victory ? "" : defeatPhrases[Random.Range(0, defeatPhrases.Count)]; 
    }
    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void DisplayMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }



}
