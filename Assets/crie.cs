using UnityEngine;

public class CapybaraController : MonoBehaviour
{
    public GameObject objectToSpawn1; // Référence au premier objet à faire apparaître
    public GameObject objectToSpawn2; // Référence au deuxième objet à faire apparaître
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

        // Activez le premier objet à faire apparaître
        if (objectToSpawn1 != null)
        {
            Debug.Log("Activating object 1.");
            objectToSpawn1.SetActive(true);
        }
        else
        {
            Debug.LogError("objectToSpawn1 is null.");
        }

        // Activez le deuxième objet à faire apparaître
        if (objectToSpawn2 != null)
        {
            Debug.Log("Activating object 2.");
            objectToSpawn2.SetActive(true);
        }
        else
        {
            Debug.LogError("objectToSpawn2 is null.");
        }

        // Désactivez les objets après une seconde
        Invoke("DeactivateObjects", 1f);
    }

    void DeactivateObjects()
    {
        // Désactivez le premier objet à faire apparaître
        if (objectToSpawn1 != null)
        {
            Debug.Log("Deactivating object 1.");
            objectToSpawn1.SetActive(false);
        }

        // Désactivez le deuxième objet à faire apparaître
        if (objectToSpawn2 != null)
        {
            Debug.Log("Deactivating object 2.");
            objectToSpawn2.SetActive(false);
        }
    }
}
