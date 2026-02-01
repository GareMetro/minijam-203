using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Unity.VectorGraphics.Scene;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LevelSelectMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject CreditsMenu;

    public void PlayLevel(String levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenLevelSelect()
    {
        MainMenu.SetActive(false);
        LevelSelectMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        MainMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenu.SetActive(false);
        LevelSelectMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelectMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
