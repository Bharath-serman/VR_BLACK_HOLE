using UnityEngine;

public class PlanetPullEffect : MonoBehaviour
{
    private Transform blackHole; // Black hole's position
    private float pullSpeed; // Speed of the pull
    private float acceleration; // Acceleration of the pull
    private bool isBeingPulled = false; // Toggle pull effect
    private Vector3 randomPullDirection; // Randomized pull direction

    void Start()
    {
        // Randomize initial pull direction
        randomPullDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    void Update()
    {
        if (isBeingPulled && blackHole != null)
        {
            Vector3 pullDirection = (blackHole.position - transform.position).normalized;

            // Add a slight random deviation for realism
            Vector3 adjustedPullDirection = (pullDirection + randomPullDirection * 0.2f).normalized;

            // Increase pull speed over time
            pullSpeed += acceleration * Time.deltaTime;

            // Move the planet toward the black hole
            transform.position += adjustedPullDirection * pullSpeed * Time.deltaTime;

            // Optional: Add rotation for more realism
            transform.Rotate(Vector3.up * 50f * Time.deltaTime);

            // Check if the planet has reached the black hole
            if (Vector3.Distance(transform.position, blackHole.position) <= 0.5f)
            {
                Destroy(gameObject); // Destroy the planet once it "enters" the black hole
            }
        }
    }

    public void StartPulling(Transform blackHole, float initialPullSpeed, float pullAcceleration)
    {
        this.blackHole = blackHole;
        this.pullSpeed = initialPullSpeed;
        this.acceleration = pullAcceleration;
        this.isBeingPulled = true;
    }
}
