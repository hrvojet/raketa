using CheckpointStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using StopwatchTime;
using CheckpointStorage;

namespace RocketGameLevelManager
{
    public class GameLoader: MonoBehaviour
    {
        private static float PreservedTimeBetweenScenes;
        public static void LoadFirstLevel()
        {
            SceneManager.LoadScene(1);
        }

        public static void ResetGame(bool gotToken)
        {
            if (gotToken)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
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
    }
}