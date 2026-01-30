using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Unity.VectorGraphics.Scene;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private String gameSceneName;

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject CreditsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenCredits()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
