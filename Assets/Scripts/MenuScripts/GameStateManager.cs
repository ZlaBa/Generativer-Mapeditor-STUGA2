using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameStateManager : MonoBehaviour
{
    public GameObject _inGameMenu;
    public GameObject _inGameAnleitung;
    public GameObject GameOverScreen;
    public GameObject YouWinScreen;

    bool GameIsOver = false;

    public void GameOver()
    {
        if (GameIsOver == false)
        {
            GameIsOver = true;
            Debug.Log("Du hesch verlore!");
            GameOverScreen.SetActive(true);
        }

    }

    bool GameIsPaused = false;

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Time.timeScale = 1;
            GameIsPaused = false;
        } else
        {
            Time.timeScale = 0;
            GameIsPaused = true;
        }
    }

    public void Restart()
    {
        Debug.Log("Restart aktiviert.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Szene wurde neu gestartet.");
        GameOverScreen.SetActive(false);
        Debug.Log("GameOverScreen wurde ausgeblendet.");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void YouWin()
    {
        Debug.Log("Du hesches gschafft!");
        YouWinScreen.SetActive(true);
        PauseGame();
    }


    public void OpenInGameMenu()
    {
        _inGameMenu.SetActive(true);
        PauseGame();
    }

    public void BackToGame()
    {
        PauseGame();
        _inGameMenu.SetActive(false);
        _inGameAnleitung.SetActive(false);
    }

    public void InGameAnleitung()
    {
        _inGameAnleitung.SetActive(true);
    }

    public void InGameAnleitungClose()
    {
        _inGameAnleitung.SetActive(false);
    }

    public void InGameBackToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenInGameMenu();
        }
    }
}
