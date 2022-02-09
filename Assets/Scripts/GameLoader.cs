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

        public static void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = ++currentSceneIndex;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}