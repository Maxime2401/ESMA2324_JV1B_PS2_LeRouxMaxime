using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Nom de la scène à charger
    private bool playerInTrigger = false; // Indique si le joueur est dans la zone de déclenchement

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si le GameObject en contact a le tag "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Vérifier si le GameObject en contact a le tag "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger)
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("La touche T est enfoncée. Changing scene...");
                // Charger la scène spécifiée
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
