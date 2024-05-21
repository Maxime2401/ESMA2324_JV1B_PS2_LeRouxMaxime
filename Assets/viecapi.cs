using UnityEngine;
using System.Collections;

public class barvi : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public int health;
    public float invincibleflashdelay = 0.1f;
    public bool invincible = false;
    public Vector3 respawnPosition; // Position de respawn
    public SpriteRenderer graphics;
    public HealthBar HealthBar;
    public GameObject currentCheckpoint; // Référence au dernier checkpoint activé

    void Start()
    {
        // Définir la position de réapparition initiale au démarrage du jeu
        SetInitialSpawnPosition();
        // Initialiser la santé actuelle au maximum au démarrage
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);
    }

    void SetInitialSpawnPosition()
    {
        // Assigner une position initiale arbitraire
        respawnPosition = transform.position;
    }

    void Update() // teste
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(1);
        }
    }

    public void TakeEnergie(int energie)
    {
        currentHealth += energie;
        // Vérifier si la santé actuelle dépasse la santé maximale
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Réinitialiser la santé à la valeur maximale
        }
        HealthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            currentHealth -= damage;
            HealthBar.SetHealth(currentHealth);
            StartCoroutine(EnableInvincibilityWithDelay());

            if (currentHealth <= 0)
            {
                Debug.Log("Player health reached 0. Teleporting player...");
                TeleportPlayer(); // téléportation si la santé est <= 0
            }
        }
    }

    public IEnumerator invincibleFrash()
    {
        while (invincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibleflashdelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibleflashdelay);
        }
    }

    public IEnumerator HandinvincibleDelay()
    {
        yield return new WaitForSeconds(0.3f); // temps d'invincibilité
        invincible = false;
    }

    void TeleportPlayer()
    {
        Debug.Log("Teleporting player to checkpoint...");
        transform.position = respawnPosition; // Téléporte le joueur à la position de respawn
        currentHealth = maxHealth; // réinitialiser la vie du joueur
        HealthBar.SetHealth(currentHealth);
        Debug.Log("Player teleported!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint touched!");
            SetCheckpoint(other.gameObject);
        }
    }

    IEnumerator EnableInvincibilityWithDelay()
    {
        // Attendre une demi-seconde
        yield return new WaitForSeconds(0.1f);

        // Après avoir attendu, activer l'invincibilité
        Debug.Log("Touché par un ennemi");
        invincible = true;
        StartCoroutine(invincibleFrash());

        // Démarrer la coroutine pour désactiver l'invincibilité après un certain temps
        StartCoroutine(DisableInvincibility());
    }

    IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(2f); // Attendre pendant 3 secondes
        invincible = false; // Désactiver l'invincibilité
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        Debug.Log("Setting new checkpoint...");
        currentCheckpoint = checkpoint; // Définit le nouveau checkpoint
        respawnPosition = checkpoint.transform.position; // Met à jour la position de respawn
        Debug.Log("Checkpoint position: " + respawnPosition);
    }
}
