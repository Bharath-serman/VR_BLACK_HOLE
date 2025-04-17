using UnityEngine;
using System.Collections;

public class BlackHoleLensDistortion : MonoBehaviour
{
    public Material distortionMaterial; 
    public float maxDistortion = 1.0f; 
    public float effectDuration = 5.0f; 
    public float radialWarpStrength = 0.3f; 

    private float currentDistortion = 0.0f;
    private bool isDistorting = false;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (distortionMaterial != null)
        {
            distortionMaterial.SetFloat("_DistortionStrength", currentDistortion);
            distortionMaterial.SetFloat("_RadialWarp", radialWarpStrength * currentDistortion);
            Graphics.Blit(source, destination, distortionMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void TriggerBlackHoleEffect()
    {
        if (!isDistorting)
            StartCoroutine(DistortEffect());
    }

    private IEnumerator DistortEffect()
    {
        isDistorting = true;
        float elapsedTime = 0f;

        // **Approaching the event horizon** (distortion increases exponentially)
        while (elapsedTime < effectDuration)
        {
            float t = elapsedTime / effectDuration;
            currentDistortion = Mathf.Pow(t, 2.5f) * maxDistortion; // Exponential curve for acceleration
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f); // Hold effect at peak

        // **Crossing the event horizon** (distortion maxes out and fades)
        elapsedTime = 0f;
        while (elapsedTime < effectDuration)
        {
            float t = elapsedTime / effectDuration;
            currentDistortion = (1 - Mathf.Pow(t, 2.5f)) * maxDistortion; // Reverse exponential curve
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDistorting = false;
    }
}
