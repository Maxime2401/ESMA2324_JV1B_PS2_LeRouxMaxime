using UnityEngine;
using UnityEngine.UI;

public class PelicanController : MonoBehaviour
{
    public float flyDuration = 5f; // Durée maximale de vol
    public float flyForce = 5f; // Force de vol appliquée au pélican
    public float horizontalSpeed = 3f; // Vitesse de déplacement horizontal
    public Image flyBar; // Référence à la barre de vol UI

    private Rigidbody2D rb;
    private bool isFlying = false; // Indique si le pélican est en train de voler
    private float flyTimer = 0f; // Compteur pour la durée de vol
    private bool canFly = true; // Indique si le pélican peut voler

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flyBar.fillAmount = 1f; // Remplir la barre de vol au début
    }

    void Update()
    {
        HandleMovement();
        HandleFlying();
        UpdateFlyBar();
    }

    void HandleFlying()
    {
        if (Input.GetKey(KeyCode.Space) && canFly && flyBar.fillAmount > 0f)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }

        if (isFlying && flyBar.fillAmount > 0f)
        {
            flyTimer += Time.deltaTime;
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Force);

            // Diminue la barre de vol
            flyBar.fillAmount -= Time.deltaTime / flyDuration;

            if (flyBar.fillAmount <= 0f)
            {
                canFly = false; // Désactive la possibilité de voler jusqu'à ce que le pélican touche le sol
            }
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * horizontalSpeed, rb.velocity.y);
    }

    void UpdateFlyBar()
    {
        // Recharge la barre de vol lorsqu'il est au sol
        if (canFly && flyBar.fillAmount < 1f)
        {
            flyBar.fillAmount += Time.deltaTime / flyDuration;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canFly = true; // Réactive la possibilité de voler
            flyTimer = 0f; // Réinitialise le timer de vol

            // Recharge instantanée de la barre de vol
            flyBar.fillAmount = 1f;
        }
    }
}
