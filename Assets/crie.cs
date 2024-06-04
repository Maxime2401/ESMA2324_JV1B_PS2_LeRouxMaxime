using UnityEngine;

public class CapybaraController : MonoBehaviour
{
    public GameObject objectToSpawn; // Référence à l'objet à faire apparaître
    public AudioClip screamSound; // Son du cri du capybara
    public AudioSource audioSource; // Référence à l'AudioSource du capybara

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtenez la référence à l'AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Si le joueur appuie sur une touche pour faire crier le capybara
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key pressed. Attempting to scream.");
            // Faites crier le capybara
            Scream();
        }
    }

    void Scream()
    {
        // Jouez le son du cri
        if (audioSource != null && screamSound != null)
        {
            Debug.Log("Playing scream sound.");
            audioSource.PlayOneShot(screamSound);
        }
        else
        {
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is null.");
            }
            if (screamSound == null)
            {
                Debug.LogError("screamSound is null.");
            }
        }

        // Activez l'objet à faire apparaître
        if (objectToSpawn != null)
        {
            Debug.Log("Activating object.");
            objectToSpawn.SetActive(true);

            // Désactivez l'objet après une seconde
            Invoke("DeactivateObject", 1f);
        }
        else
        {
            Debug.LogError("objectToSpawn is null.");
        }
    }

    void DeactivateObject()
    {
        // Désactivez l'objet à faire apparaître
        if (objectToSpawn != null)
        {
            Debug.Log("Deactivating object.");
            objectToSpawn.SetActive(false);
        }
    }
}
