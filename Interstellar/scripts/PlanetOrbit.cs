using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f; // Speed of planet's self-rotation

    [Header("Revolution Settings")]
    public Transform orbitPath; // Parent object defining the orbit
    public float revolutionSpeed = 20f; // Speed at which the planet revolves around the orbit path

    private float currentAngle = 0f; // To track revolution angle

    void Update()
    {
        // Rotate the planet around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);

        // Revolution: Move the planet along the orbit path
        if (orbitPath != null)
        {
            currentAngle += revolutionSpeed * Time.deltaTime; // Update angle
            currentAngle %= 360f; // Keep angle within 0-360 degrees

            // Calculate new position based on orbit's radius
            Vector3 orbitPosition = orbitPath.position;
            float orbitRadius = Vector3.Distance(transform.position, orbitPath.position);

            // Move the planet in a circular orbit
            float x = orbitRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float z = orbitRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            transform.position = new Vector3(orbitPosition.x + x, transform.position.y, orbitPosition.z + z);
        }
    }
}
