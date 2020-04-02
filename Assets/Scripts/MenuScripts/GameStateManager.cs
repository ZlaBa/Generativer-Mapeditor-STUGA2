using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameStateManager : MonoBehaviour
{
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
    }
}
