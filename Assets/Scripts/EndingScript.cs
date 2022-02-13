using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RocketGameLevelManager;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{

    private float _finalTime;
    public Text time;
    
    // Start is called before the first frame update
    void Start()
    {
        _finalTime = GameLoader.GetPreservedTime() - 2;
        TimeSpan timeSpan = TimeSpan.FromSeconds(_finalTime);
        time.text = timeSpan.ToString(@"mm\:ss\:fff");
        Debug.Log("Final time: " + _finalTime);
    }
}
