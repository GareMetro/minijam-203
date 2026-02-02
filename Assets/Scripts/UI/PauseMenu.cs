using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class PauseMenu : MonoBehaviour
{ 
    public void DisplayMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Return()
    {
        GameManager.Instance.HideOrShowPauseMenu();
    }
}
