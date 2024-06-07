using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class barvi : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public float invincibleflashdelay = 0.1f;
    public bool invincible = false;
    public Vector3 respawnPosition; 
    public SpriteRenderer graphics;
    public HeartDisplay heartDisplay;
    public GameObject currentCheckpoint;
    public Image transitionCircle;
    public float transitionDuration = 1f; 
    public Animator animator;
    public AudioClip damageSound; 
    private AudioSource audioSource; 

    public GameObject objectToReset; // Objet à réinitialiser
    private Vector3 initialObjectPosition; // Position initiale de l'objet

    void Start()
    {
        SetInitialSpawnPosition();
        currentHealth = maxHealth;
        heartDisplay.UpdateHearts(currentHealth);

        if (transitionCircle != null)
        {
            transitionCircle.rectTransform.localScale = Vector3.zero;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = damageSound;

        initialObjectPosition = objectToReset.transform.position; // Stocke la position initiale de l'objet
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
        heartDisplay.UpdateHearts(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            currentHealth -= damage;
            heartDisplay.UpdateHearts(currentHealth);

            if (currentHealth <= 0)
            {
                animator.SetBool("isDead", true); 
                StartCoroutine(HandleTeleportationWithTransition(2f)); 
            }
            else
            {
                StartCoroutine(EnableInvincibilityWithDelay());
            }

            if (damageSound != null)
            {
                audioSource.Play();
            }
        }
    }

    IEnumerator HandleTeleportationWithTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ScaleTransitionCircle(Vector3.one, transitionDuration));
        animator.SetBool("isDead", false);
        ResetObject(); // Réinitialise l'objet à sa position initiale
        TeleportPlayer();
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
        transform.position = respawnPosition;
        currentHealth = maxHealth;
        heartDisplay.UpdateHearts(currentHealth);
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
        animator.SetBool("isDead", false); 
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

    void ResetObject()
    {
        objectToReset.transform.position = initialObjectPosition; // Réinitialise l'objet à sa position initiale
    }
}
