using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public Image fadeOverlay; // Assign the FadeOverlay image in the Inspector
    public float fadeDuration = 10.0f; // Duration of the fade

    private void Start()
    {
        // Ensure the overlay is fully transparent at the start
        if (fadeOverlay != null)
        {
            Color color = fadeOverlay.color;
            color.a = 0;
            fadeOverlay.color = color;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1)); // Fade from transparent to opaque
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0)); // Fade from opaque to transparent
    }

    private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeOverlay.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        color.a = endAlpha;
        fadeOverlay.color = color;
    }
}
