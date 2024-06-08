using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float invincibilityDuration = 1f;
    public Color invincibilityColor = Color.gray;
    public Color normalColor = Color.white;
    public float deathFlyDuration = 2f; // Durée de l'animation de vol
    public float flySpeed = 5f; // Vitesse de vol
    private float rotationSpeed = 720f; // Vitesse de rotation en degrés par seconde

    public int currentHealth { get; private set; }
    public bool isDead { get; private set; } = false;
    public bool isInvincible { get; private set; } = false;

    public SpriteRenderer spriteRenderer;
    private Collider2D enemyCollider;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>(); // Récupère le collider de l'ennemi
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        spriteRenderer.color = invincibilityColor;

        Debug.Log("Enemy is now invincible for " + invincibilityDuration + " seconds.");

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
        spriteRenderer.color = normalColor;

        Debug.Log("Enemy is no longer invincible.");
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy died!");

        // Désactiver le collider
        enemyCollider.enabled = false;

        // Lancer la coroutine d'animation de mort
        StartCoroutine(DeathFlyCoroutine());
    }

    IEnumerator DeathFlyCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < deathFlyDuration)
        {
            // Déplacement en diagonale droite
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime + Vector3.right * flySpeed * Time.deltaTime, Space.World);
            // Rotation
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Détruire l'objet après l'animation
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dommage"))
        {
            Debug.Log("Enemy collided with a damage dealer.");
            TakeDamage(1);
        }
    }
}
