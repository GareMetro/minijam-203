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

    [SerializeField] private List<String> defeatPhrases = new(); 

    public void DisplayEndGame(bool victory)
    {
        
        resultTextContainer.text = victory ? "Victory" : "Defeat";
        
        defeatPhraseContainer.text = victory ? "" : defeatPhrases[Random.Range(0, defeatPhrases.Count)]; 
    }

    public void DisplayMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
