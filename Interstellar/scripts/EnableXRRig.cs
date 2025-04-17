using UnityEngine;

public class ActivateXRRig : MonoBehaviour
{
    [SerializeField] private GameObject xrRig;

    void Start()
    {
        if (xrRig != null)
        {
            xrRig.SetActive(true); // Enable XR Rig when the scene loads
        }
    }
}
