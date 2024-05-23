using UnityEngine;
using System.Collections;

public class ActivateGameObject : MonoBehaviour
{
    public GameObject objectToActivate; // Le GameObject à activer
    public float activationDelay = 2f; // Délai en secondes avant l'activation
    public float activeDuration = 1f; // Durée en secondes pendant laquelle le GameObject est actif

    private bool isPlayerTouching = false; // Indique si le joueur est en contact avec le GameObject

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            isPlayerTouching = true; // Le joueur est en contact avec le GameObject
            StartCoroutine(ActivateObjectCoroutine()); // Lancer la coroutine pour activer le GameObject
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone.");
            isPlayerTouching = false; // Le joueur n'est plus en contact avec le GameObject
        }
    }

    private IEnumerator ActivateObjectCoroutine()
    {
        Debug.Log("Coroutine started. Waiting for activation delay: " + activationDelay + " seconds.");
        yield return new WaitForSeconds(activationDelay); // Attendre le délai d'activation

        if (isPlayerTouching) // Vérifie de nouveau si le joueur est toujours en contact avant d'activer
        {
            Debug.Log("Activation delay complete. Activating GameObject for duration: " + activeDuration + " seconds.");
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // Activer le GameObject
                Debug.Log("Object activated: " + objectToActivate.name);
            }
            else
            {
                Debug.LogWarning("objectToActivate is not assigned.");
            }

            yield return new WaitForSeconds(activeDuration); // Attendre la durée d'activation

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false); // Désactiver le GameObject
                Debug.Log("Object deactivated: " + objectToActivate.name);
            }
            else
            {
                Debug.LogWarning("objectToActivate is not assigned.");
            }
        }
    }
}
