using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RocketGameLevelManager;
using UnityEngine.SceneManagement;

namespace StopwatchTime
{
    
    public class StopWatch : MonoBehaviour
    {
        bool stopWatchActive = false;
        float currentTime;
        public Text currentTimeText;

        private void Start()
        {
            currentTime = GameLoader.GetPreservedTime();
            StartStopwatch();
        }

        private void Update()
        {
            if (stopWatchActive)
            {
                currentTime += Time.deltaTime;
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        }

        public void StartStopwatch()
        {
            stopWatchActive = true;
        }
        
        public void StopStopwatch()
        {
            stopWatchActive = false;
        }

        private void OnDestroy()
        {
            Debug.Log("Scene from stopwatch script: " + SceneManager.GetActiveScene().buildIndex);
            GameLoader.SetPreservedTime(currentTime);
        }
    }
}