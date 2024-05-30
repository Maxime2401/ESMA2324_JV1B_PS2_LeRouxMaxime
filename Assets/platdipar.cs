using UnityEngine;

public class DisappearAndReappear : MonoBehaviour
{
    public float disappearTime = 3f; // Time in seconds before the GameObject disappears after being touched by the player
    public float reappearDelay = 5f; // Delay in seconds before the GameObject reappears
    private bool isDisabled = false; // Indicates if the GameObject is disabled

    void Start()
    {
        isDisabled = false; // Ensure the GameObject starts enabled
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Disappear", disappearTime); // Schedule the GameObject to disappear after the specified time
        }
    }

    // Function to disable the GameObject
    void Disappear()
    {
        gameObject.SetActive(false); // Disable the GameObject
        isDisabled = true; // Mark the GameObject as disabled
        Invoke("Reappear", reappearDelay); // Schedule the GameObject to reappear after the specified delay
    }

    // Function to re-enable the GameObject
    void Reappear()
    {
        gameObject.SetActive(true); // Re-enable the GameObject
        isDisabled = false; // Mark the GameObject as enabled
    }
}
