using UnityEngine;

public class Spaghettification : MonoBehaviour
{
    public Transform blackHole; // Assign the black hole object
    public float effectRadius = 50f; // Radius within which the effect starts
    public float stretchFactor = 5f; // Maximum stretch amount
    public float compressionFactor = 0.2f; // Minimum compression scale

    void Update()
    {
        // Find all planets tagged "Planet"
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            float distance = Vector3.Distance(planet.transform.position, blackHole.position);

            if (distance < effectRadius)
            {
                // Calculate the amount of stretching/compression based on distance
                float t = Mathf.InverseLerp(effectRadius, 0, distance); // Normalized distance
                float stretch = Mathf.Lerp(1, stretchFactor, t); // Stretch more as distance decreases
                float compress = Mathf.Lerp(1, compressionFactor, t); // Compress more as distance decreases

                // Stretch along the axis toward the black hole
                Vector3 direction = (blackHole.position - planet.transform.position).normalized;
                Vector3 scale = planet.transform.localScale;

                scale.x = Mathf.Lerp(scale.x, direction.x != 0 ? stretch : compress, t);
                scale.y = Mathf.Lerp(scale.y, compress, t);
                scale.z = Mathf.Lerp(scale.z, compress, t);

                planet.transform.localScale = scale;

                // Gradually move the planet closer to the black hole
                Rigidbody rb = planet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * (stretchFactor / distance), ForceMode.Acceleration);
                }
            }
        }
    }
}
