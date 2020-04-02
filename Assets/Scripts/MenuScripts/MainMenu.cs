using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject BackgroundColor;
    public GameObject MenuBackground;
    public GameObject StartMenu;
    public GameObject AnleitungMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Anleitung()
    {
        Debug.Log("Anleitung aktiviert.");
        StartMenu.SetActive(false);
        MenuBackground.SetActive(false);
        AnleitungMenu.SetActive(true);
    }

    public void BackToMain()
    {
        Debug.Log("Zurück zum Hauptmenu.");
        StartMenu.SetActive(true);
        MenuBackground.SetActive(true);
        AnleitungMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Ende, aus!");
        Application.Quit();
    }
}
