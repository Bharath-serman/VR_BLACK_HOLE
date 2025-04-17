using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class BlackHoleEffect : MonoBehaviour
{
    public PostProcessVolume postProcessingVolume;
    private LensDistortion lensDistortion;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private MotionBlur motionBlur;

    public float maxDistortion = -50f;
    public float maxVignette = 0.5f;
    public float maxChromatic = 1f;
    public float maxMotionBlur = 0.8f;
    public float effectSpeed = 2f;

    void Start()
    {
        if (postProcessingVolume != null)
        {
            postProcessingVolume.profile.TryGetSettings(out lensDistortion);
            postProcessingVolume.profile.TryGetSettings(out vignette);
            postProcessingVolume.profile.TryGetSettings(out chromaticAberration);
            postProcessingVolume.profile.TryGetSettings(out motionBlur);
        }

        if (lensDistortion != null) lensDistortion.intensity.value = 0f;
        if (vignette != null) vignette.intensity.value = 0f;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;
        if (motionBlur != null) motionBlur.shutterAngle.value = 0f;
    }

    public void StartBlackHoleEffect()
    {
        StartCoroutine(ApplyEffects());
    }

    private IEnumerator ApplyEffects()
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime * effectSpeed;

            if (lensDistortion != null) 
                lensDistortion.intensity.value = Mathf.Lerp(0, maxDistortion, t);
            if (vignette != null) 
                vignette.intensity.value = Mathf.Lerp(0, maxVignette, t);
            if (chromaticAberration != null) 
                chromaticAberration.intensity.value = Mathf.Lerp(0, maxChromatic, t);
            if (motionBlur != null) 
                motionBlur.shutterAngle.value = Mathf.Lerp(0, maxMotionBlur * 360, t);

            yield return null;
        }
    }
}
