using UnityEngine;

public class BlackHoleManager : MonoBehaviour
{
    public Transform blackHole; // Reference to the black hole's position
    public float pullSpeed = 2f; // Base speed of the pull
    public float acceleration = 0.5f; // Acceleration of the pull

    // Trigger the pull effect for all planets
    public void PullAllPlanets()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            PlanetPullEffect pullEffect = planet.GetComponent<PlanetPullEffect>();

            if (pullEffect != null)
            {
                pullEffect.StartPulling(blackHole, pullSpeed, acceleration);
            }
        }

        Debug.Log("All planets are being pulled toward the black hole!");
    }
}
