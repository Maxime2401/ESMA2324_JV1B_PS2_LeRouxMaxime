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
    public Animator animator; // Référence à l'Animator

    public GameObject itemToReset; // L'objet à réinitialiser
    private Vector3 initialItemPosition; // La position initiale de l'objet

    void Start()
    {
        SetInitialSpawnPosition();
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
        HealthBar.SetHealth(currentHealth);

        if (transitionCircle != null)
        {
            transitionCircle.rectTransform.localScale = Vector3.zero;
        }

        if (itemToReset != null)
        {
            initialItemPosition = itemToReset.transform.position;
        }
    }

    void SetInitialSpawnPosition()
    {
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
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
                animator.SetBool("isDead", true); // Déclencher l'animation de mort
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
        // Démarrer la transition circulaire entrante

        // Attendre le délai avant de téléporter
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ScaleTransitionCircle(Vector3.one, transitionDuration));
        animator.SetBool("isDead", false);
        TeleportPlayer();

        // Démarrer la transition circulaire sortante
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
        transform.position = respawnPosition; // Téléporter le joueur à la position de respawn
        currentHealth = maxHealth; // Réinitialiser la vie du joueur
        HealthBar.SetHealth(currentHealth);

        if (itemToReset != null)
        {
            itemToReset.transform.position = initialItemPosition; // Réinitialiser la position de l'objet
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCheckpoint(other.gameObject);
        }
    }

    IEnumerator EnableInvincibilityWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        invincible = true;
        StartCoroutine(invincibleFlash());

        yield return new WaitForSeconds(2f);
        invincible = false;
        animator.SetBool("isDead", false); // Désactiver l'animation de mort
    }

    IEnumerator invincibleFlash()
    {
        while (invincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibleflashdelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibleflashdelay);
        }
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
        respawnPosition = checkpoint.transform.position;
    }
}
