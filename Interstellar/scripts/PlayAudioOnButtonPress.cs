using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnButtonClick : MonoBehaviour
{
    public Button yourButton; // Reference to the UI Button
    public AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Ensure the AudioSource is not playing at the start
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Add a listener to the button to call the PlayAudio method when clicked
        yourButton.onClick.AddListener(PlayAudio);
    }

    void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
