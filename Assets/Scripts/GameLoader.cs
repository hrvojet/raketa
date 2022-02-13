using CheckpointStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using StopwatchTime;
using CheckpointStorage;
using UnityEditor.UI;
using StopwatchTime;
using CheckpointStorage;

namespace RocketGameLevelManager
{
    public class GameLoader: MonoBehaviour
    {
        private static float PreservedTimeBetweenScenes;
        private static float PreservedTimeBetweenScenesWithToken;
        public static void LoadFirstLevel()
        {
            SceneManager.LoadScene(1);
        }

        public static void ResetGame()
        {
            if (Token.IsTokenCollected)
            {
                PreservedTimeBetweenScenes = PreservedTimeBetweenScenesWithToken;
                SceneManager.LoadScene(4);
            }
            else
            {
                PreservedTimeBetweenScenes = 0f;
                LoadFirstLevel();
            }
        }

        public static void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = ++currentSceneIndex;
            SceneManager.LoadScene(nextSceneIndex);
        }
        
        public static float GetEndTime()
        {
            return PreservedTimeBetweenScenes;
        }
        
        public static void SetEndTime(float timeToPreserve)
        {
            PreservedTimeBetweenScenes = timeToPreserve;
        }

        public static float GetPreservedTime()
        {
            return PreservedTimeBetweenScenes;
        }
        
        public static void SetPreservedTime(float timeToPreserve)
        {
            PreservedTimeBetweenScenes = timeToPreserve;
        }
        
        public static void SetPreservedTimeForToken(float timeToPreserve)
        {
            PreservedTimeBetweenScenesWithToken = timeToPreserve;
        }
    }
}