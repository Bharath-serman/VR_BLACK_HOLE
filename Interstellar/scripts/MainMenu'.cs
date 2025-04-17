using UnityEngine;
using UnityEngine.SceneManagement; // This is needed for scene loading

public class MainMenu : MonoBehaviour
{
    // This method will be called when the Start Game button is clicked
    public void StartGame()
    {
         Debug.Log("Starting Game...");
    Destroy(GameObject.Find("XR Rig")); // Destroy the Main Menu XR Rig
    SceneManager.LoadScene("Spidey"); // Load the black hole scene
    }


    public void spider()
    {
        SceneManager.LoadScene("earth");
    }

    // This method will be called when the Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }


}
