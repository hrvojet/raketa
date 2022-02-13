using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RocketGameLevelManager;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameLoader.SetPreservedTime(0f);
    }

    public void PlayGame()
    {
        GameLoader.LoadFirstLevel();
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
