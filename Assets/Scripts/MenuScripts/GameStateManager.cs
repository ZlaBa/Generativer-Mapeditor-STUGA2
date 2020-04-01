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
        GameOverScreen.SetActive(false);
        YouWinScreen.SetActive(false);
        SceneManager.LoadScene("MapEditor");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameOverScreen.SetActive(false);
        YouWinScreen.SetActive(false);
    }

    public void YouWin()
    {
        Debug.Log("Du hesches gschafft!");
        YouWinScreen.SetActive(true);
    }
}
