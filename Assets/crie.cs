using UnityEngine;

public class CapybaraController : MonoBehaviour
{
    public GameObject objectToSpawn; // Référence à l'objet à faire apparaître
    public AudioClip screamSound; // Son du cri du capybara
    public AudioSource audioSource; // Référence à l'AudioSource du capybara

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtenez la référence à l'AudioSource
    }

    void Update()
    {
        // Si le joueur appuie sur une touche pour faire crier le capybara
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Faites crier le capybara
            Scream();
        }
    }

    void Scream()
    {
        // Jouez le son du cri
        if (audioSource != null && screamSound != null)
        {
            audioSource.PlayOneShot(screamSound);
        }

        // Activez l'objet à faire apparaître
        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(true);

            // Désactivez l'objet après une seconde
            Invoke("DeactivateObject", 1f);
        }
    }

    void DeactivateObject()
    {
        // Désactivez l'objet à faire apparaître
        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(false);
        }
    }
}
