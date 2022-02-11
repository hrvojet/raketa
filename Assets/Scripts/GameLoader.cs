using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketGameLevelManager
{
    public class GameLoader: MonoBehaviour
    {
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
    }
}