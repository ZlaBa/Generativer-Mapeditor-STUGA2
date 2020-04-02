﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Anleitung()
    {
        Debug.Log("Anleitung aktiviert.");
    }

    public void QuitGame()
    {
        Debug.Log("Ende, aus!");
        Application.Quit();
    }
}
