using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketGameLevelManager
{
    public class GameLoader: MonoBehaviour
    {
        public static void LoadFirstLevel()
        {
            print("In rocket game level manager");
            SceneManager.LoadScene(0);
        }

        public static void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = ++currentSceneIndex;
            if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex); // todo allow more than two levels
        }
        
    }
}