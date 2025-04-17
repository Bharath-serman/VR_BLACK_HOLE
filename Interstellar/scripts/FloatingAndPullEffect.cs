using UnityEngine;

public class FloatingAndPullEffect : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatingAmplitude = 0.2f; // Distance of floating
    public float floatingSpeed = 0.5f;     // Speed of floating
    public bool randomizeDirection = true; // Enable/disable random direction

    [Header("Pull Effect Settings")]
    public Transform blackHole; // Reference to the black hole
    public float initialPullSpeed = 1f; // Initial speed of the pull
    public float maxPullSpeed = 5f;     // Maximum speed of the pull
    public float acceleration = 1.5f;  // Acceleration multiplier
    public float stopDistance = 1f;    // Distance at which pulling stops
    public bool isBeingPulled = false; // Toggle pull effect

    private Vector3 initialPosition;   // Starting position of the camera
    private Vector3 randomDirection;  // Direction for floating
    private bool hasStopped = false;  // Tracks if pulling has stopped
    private float currentPullSpeed;   // Dynamic pull speed
    private Vector3 spiralDirection;  // Direction of spiraling

    void Start()
    {
        // Store the initial position
        initialPosition = transform.position;

        // Randomize floating direction if enabled
        if (randomizeDirection)
        {
            randomDirection = new Vector3(
                Random.Range(0.5f, 1f),
                Random.Range(0.5f, 1f),
                Random.Range(0.5f, 1f)
            ).normalized;
        }
        else
        {
            randomDirection = Vector3.up; // Default to up-down floating
        }

        // Initialize pull speed and spiral direction
        currentPullSpeed = initialPullSpeed;
        spiralDirection = Vector3.Cross(Vector3.up, blackHole.position - transform.position).normalized;
    }

    void Update()
    {
        if (isBeingPulled && !hasStopped)
        {
            // Calculate pull direction
            Vector3 pullDirection = (blackHole.position - transform.position).normalized;

            // Apply spiraling effect
            Vector3 spiralOffset = Vector3.Cross(pullDirection, spiralDirection) * Mathf.Sin(Time.time * currentPullSpeed);

            // Increase the pull speed based on proximity to the black hole
            currentPullSpeed = Mathf.Min(maxPullSpeed, currentPullSpeed + acceleration * Time.deltaTime);

            // Move the camera with spiraling and pulling
            transform.position += (pullDirection + spiralOffset) * currentPullSpeed * Time.deltaTime;

            // Check if the camera is close enough to stop pulling
            if (Vector3.Distance(transform.position, blackHole.position) <= stopDistance)
            {
                StopPulling();
            }
        }
        else if (!isBeingPulled && !hasStopped)
        {
            // Apply floating effect
            float offset = Mathf.Sin(Time.time * floatingSpeed) * floatingAmplitude;
            transform.position = initialPosition + randomDirection * offset;
        }
    }

    // Public method to activate pull effect
    public void StartPulling()
    {
        isBeingPulled = true;
        hasStopped = false;
        currentPullSpeed = initialPullSpeed; // Reset pull speed
    }

    // Method to stop pulling
    private void StopPulling()
    {
        isBeingPulled = false;
        hasStopped = true;
        transform.position = blackHole.position; // Snap to the black hole position
        Debug.Log("Pulling stopped: Camera reached the black hole.");
    }
}
