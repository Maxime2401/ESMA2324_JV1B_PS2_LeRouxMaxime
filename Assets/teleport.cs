using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination; // Point de téléportation
    public KeyCode teleportKey = KeyCode.T; // Touche pour téléporter

    void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            Teleport();
        }
    }

    void Teleport()
    {
        if (teleportDestination != null)
        {
            transform.position = teleportDestination.position;
        }
        else
        {
            Debug.LogWarning("No teleport destination set.");
        }
    }
}
