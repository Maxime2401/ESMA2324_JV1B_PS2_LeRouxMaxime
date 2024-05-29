using UnityEngine;

public class HealthRecoveryItem : MonoBehaviour
{
    public int healthToRecover = 5; // Quantité de santé à récupérer

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si la collision est avec le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected!");

            // Récupérer le script barvi attaché au joueur
            barvi playerHealth = other.GetComponent<barvi>();

            // Vérifier si le script barvi a été trouvé
            if (playerHealth != null)
            {
                Debug.Log("barvi script found on player!");

                // Ajouter de la santé au joueur
                playerHealth.TakeEnergie(healthToRecover);
                Debug.Log("Recovered " + healthToRecover + " health points!");

                // Détruire cet objet une fois qu'il a été ramassé
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("barvi script not found on player!");
            }
        }
    }
}
