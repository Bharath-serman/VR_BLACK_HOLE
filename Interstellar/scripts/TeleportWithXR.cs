using UnityEngine;

public class TeleportWithXR : MonoBehaviour
{
    public Transform xrRig; // XR Rig or Origin transform
    public Transform teleportDestination; // Teleport destination inside the tesseract
    public CameraFade cameraFade; // Reference to the CameraFade script
    public float fadeDuration = 1.0f; // Duration of fade effect

    public void Teleport()
    {
        StartCoroutine(TeleportWithFade());
    }

    private System.Collections.IEnumerator TeleportWithFade()
    {
        // Start fade-out
        if (cameraFade != null)
        {
            cameraFade.FadeOut();
            yield return new WaitForSeconds(fadeDuration);
        }

        // Teleport the XR Rig
        if (xrRig != null && teleportDestination != null)
        {
            Vector3 offset = xrRig.position - Camera.main.transform.position;
            xrRig.position = teleportDestination.position + offset; // Adjust to maintain user height
            xrRig.rotation = teleportDestination.rotation; // Match orientation if needed
        }

        // Start fade-in
        if (cameraFade != null)
        {
            cameraFade.FadeIn();
            yield return new WaitForSeconds(fadeDuration);
        }
    }
}
