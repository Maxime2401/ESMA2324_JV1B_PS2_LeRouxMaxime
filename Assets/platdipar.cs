using UnityEngine;

public class DisappearAndReappear : MonoBehaviour
{
    public float disappearTime = 3f; // Temps en secondes avant que le GameObject ne disparaisse
    public float reappearDelay = 5f; // Délai en secondes avant que le GameObject ne réapparaisse
    private bool isDisabled = false; // Indique si le GameObject est désactivé

    private float timeSincePlayerEntered = 0f; // Temps écoulé depuis que le joueur est entré dans la zone
    private bool playerInside = false; // Indique si le joueur est à l'intérieur du GameObject

    void Update()
    {
        // Si le joueur est à l'intérieur et que le temps écoulé dépasse le temps de disparition
        if (playerInside && !isDisabled && timeSincePlayerEntered >= disappearTime)
        {
            gameObject.SetActive(false); // Désactiver le GameObject
            isDisabled = true; // Marquer le GameObject comme désactivé
            Invoke("Reappear", reappearDelay); // Appeler la fonction pour réactiver après le délai spécifié
        }

        // Si le joueur n'est pas à l'intérieur et que le GameObject est désactivé
        if (!playerInside && isDisabled)
        {
            isDisabled = false; // Marquer le GameObject comme réactivé
        }

        // Incrémenter le temps écoulé
        timeSincePlayerEntered += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true; // Le joueur est à l'intérieur
            timeSincePlayerEntered = 0f; // Réinitialiser le temps écoulé
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false; // Le joueur n'est plus à l'intérieur
            timeSincePlayerEntered = 0f; // Réinitialiser le temps écoulé
        }
    }

    // Fonction pour réactiver le GameObject
    void Reappear()
    {
        gameObject.SetActive(true); // Réactiver le GameObject
    }
}
