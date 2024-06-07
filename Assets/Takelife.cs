using UnityEngine;

public class HealthRecoveryItem : MonoBehaviour
{
    public int healthToRecover = 5; // Quantité de santé à récupérer
    public AudioClip pickupSound; // Son à jouer lorsque le joueur ramasse cet objet
    private AudioSource audioSource; // AudioSource pour jouer le son

    void Start()
    {
        // Initialisation de l'AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = pickupSound;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si la collision est avec le joueur
        if (other.CompareTag("Player"))
        {
            // Récupérer le script barvi attaché au joueur
            barvi playerHealth = other.GetComponent<barvi>();

            // Vérifier si le script barvi a été trouvé
            if (playerHealth != null)
            {
                // Ajouter de la santé au joueur
                playerHealth.TakeEnergie(healthToRecover);

                // Jouer le son de ramassage

                audioSource.Play();


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
