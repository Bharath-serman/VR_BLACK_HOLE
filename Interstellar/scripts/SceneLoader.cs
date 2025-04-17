using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Public methods to be assigned to buttons
    public void LoadBlackHoleScene()
    {
        Debug.Log("Loading Black Hole Scene...");
        SceneManager.LoadScene("SPIDEY_"); // Replace with the exact name of your scene
    }

    
}
