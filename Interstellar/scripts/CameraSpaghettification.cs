using UnityEngine;

public class CameraSpaghettification : MonoBehaviour
{
    [SerializeField] private Transform blackHole; // Reference to the black hole
    [SerializeField] private float effectRadius = 50f; // Radius for spaghettification
    [SerializeField] private float maxStretch = 1.5f; // Maximum vertical stretch
    [SerializeField] private float maxCompression = 0.5f; // Minimum horizontal compression

    private Camera mainCamera;
    private Matrix4x4 originalProjectionMatrix;

    void Start()
    {
        // Get the camera component
        mainCamera = GetComponent<Camera>();

        // Store the original projection matrix
        if (mainCamera != null)
        {
            originalProjectionMatrix = mainCamera.projectionMatrix;
        }
    }

    void Update()
    {
        if (mainCamera == null || blackHole == null) return;

        // Calculate the distance between the camera and the black hole
        float distance = Vector3.Distance(transform.position, blackHole.position);

        if (distance <= effectRadius)
        {
            // Calculate distortion based on proximity
            float t = Mathf.InverseLerp(effectRadius, 0, distance);
            float verticalStretch = Mathf.Lerp(1f, maxStretch, t);
            float horizontalCompression = Mathf.Lerp(1f, maxCompression, t);

            // Create a new projection matrix
            Matrix4x4 projectionMatrix = originalProjectionMatrix;
            projectionMatrix.m00 *= horizontalCompression; // Horizontal compression
            projectionMatrix.m11 *= verticalStretch;       // Vertical stretching

            // Apply the distorted projection matrix
            mainCamera.projectionMatrix = projectionMatrix;
        }
        else
        {
            // Reset the camera to the original projection matrix
            mainCamera.projectionMatrix = originalProjectionMatrix;
        }
    }
}
