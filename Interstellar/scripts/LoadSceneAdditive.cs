using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAdditive : MonoBehaviour
{
    public string sceneName; // Assign the scene name in the inspector

    public void LoadScene()
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
