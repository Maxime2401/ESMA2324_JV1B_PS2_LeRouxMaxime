using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class barvi : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public float invincibleflashdelay = 0.1f;
    public bool invincible = false;
    public Vector3 respawnPosition; // Position de respawn
    public SpriteRenderer graphics;
    public HealthBar HealthBar;
    public GameObject currentCheckpoint; // Référence au dernier checkpoint activé
    public Image transitionCircle; // Référence à l'image circulaire pour la transition
    public float transitionDuration = 1f; // Durée de la transition

    void Start()
    {
        // Définir la position de réapparition initiale au démarrage du jeu
        SetInitialSpawnPosition();
        // Initialiser la santé actuelle au maximum au démarrage
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);

        // Assurez-vous que la transition est invisible au départ
        if (transitionCircle != null)
        {
            transitionCircle.rectTransform.localScale = Vector3.zero;
        }
    }

    void SetInitialSpawnPosition()
    {
        // Assigner une position initiale arbitraire
        respawnPosition = transform.position;
    }

    void Update()
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

            if (currentHealth <= 0)
            {
                Debug.Log("La santé du joueur est à 0. Téléportation du joueur après un délai...");
                StartCoroutine(HandleTeleportationWithTransition(2f)); // Attendre 2 secondes avant de téléporter avec transition
            }
            else
            {
                StartCoroutine(EnableInvincibilityWithDelay());
            }
        }
    }

    IEnumerator HandleTeleportationWithTransition(float delay)
    {
        // Démarrer la transition circulaire entrant
       

        // Attendre le délai avant de téléporter
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ScaleTransitionCircle(Vector3.one, transitionDuration));
        // Téléporter le joueur
        TeleportPlayer();

        // Démarrer la transition circulaire sortant
        yield return StartCoroutine(ScaleTransitionCircle(Vector3.zero, transitionDuration));
    }

    IEnumerator ScaleTransitionCircle(Vector3 targetScale, float duration)
    {
        if (transitionCircle == null)
        {
            yield break;
        }

        Vector3 startScale = transitionCircle.rectTransform.localScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transitionCircle.rectTransform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            yield return null;
        }

        transitionCircle.rectTransform.localScale = targetScale;
    }

    void TeleportPlayer()
    {
        Debug.Log("Téléportation du joueur au checkpoint...");
        transform.position = respawnPosition; // Téléporter le joueur à la position de respawn
        currentHealth = maxHealth; // Réinitialiser la vie du joueur
        HealthBar.SetHealth(currentHealth);
        Debug.Log("Joueur téléporté !");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint atteint !");
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

    IEnumerator invincibleFrash()
    {
        while (invincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibleflashdelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibleflashdelay);
        }
    }

    IEnumerator DisableInvincibility()
    {
        yield return new WaitForSeconds(2f); // Attendre pendant 2 secondes
        invincible = false; // Désactiver l'invincibilité
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        Debug.Log("Définir un nouveau checkpoint...");
        currentCheckpoint = checkpoint; // Définir le nouveau checkpoint
        respawnPosition = checkpoint.transform.position; // Mettre à jour la position de respawn
        Debug.Log("Position du checkpoint : " + respawnPosition);
    }
}
