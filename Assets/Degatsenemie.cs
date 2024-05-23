using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public float invincibilityDuration = 1f; 
    public Color invincibilityColor = Color.gray; 
    public Color normalColor = Color.white; 

    public int currentHealth { get; private set; } 
    public bool isDead { get; private set; } = false; 
    public bool isInvincible { get; private set; } = false; 

    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        currentHealth = maxHealth; 
        spriteRenderer = GetComponent<SpriteRenderer>(); 

        Debug.Log("Enemy Health initialized. Max health: " + maxHealth);
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
        Destroy(gameObject); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Dommage"))
        {
            Debug.Log("Enemy collided with a damage object.");
            TakeDamage(1); 
        }
    }
}
