using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RocketGameLevelManager;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameLoader.LoadFirstLevel();
    }
}
