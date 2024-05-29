using UnityEngine;

public class Nage : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement horizontale
    public float maxHorizontalSpeed = 3f; // Vitesse horizontale maximale
    public float jumpForce = 5f; // Force de saut pour simuler la montée
    public float floatForce = 0.5f; // Force de flottabilité pour maintenir le personnage à flot
    public Animator animator; // Référence à l'Animator
    public KeyCode moveLeftKey = KeyCode.LeftArrow; // Touche pour déplacer le personnage vers la gauche
    public KeyCode moveRightKey = KeyCode.RightArrow; // Touche pour déplacer le personnage vers la droite
    public KeyCode jumpKey = KeyCode.Space; // Touche pour nager vers le haut

    private Rigidbody2D rb;
    private bool isInWater = false; // Indique si le personnage est dans l'eau

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialisation du composant Rigidbody2D
    }

    void Update()
    {
        if (isInWater)
        {
            // Déplacement horizontal
            float moveInput = 0f;
            if (Input.GetKey(moveLeftKey))
            {
                moveInput = -1f;
            }
            else if (Input.GetKey(moveRightKey))
            {
                moveInput = 1f;
            }

            rb.AddForce(new Vector2(moveInput * moveSpeed, 0f));

            // Limiter la vitesse horizontale maximale
            float clampedXVelocity = Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
            rb.velocity = new Vector2(clampedXVelocity, rb.velocity.y);

            // Saut pour nager vers le haut
            if (Input.GetKeyDown(jumpKey))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                // Ajouter une force ascendante lente pour la flottabilité
                rb.AddForce(new Vector2(0f, -floatForce));
            }

            // Mise à jour des animations
            animator.SetFloat("VerticalSpeed", rb.velocity.y);
            if (moveInput != 0)
            {
                animator.SetBool("Swim", true);
                transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f); // Ajuste l'échelle du personnage pour qu'il regarde dans la bonne direction
            }
            else
            {
                animator.SetBool("Swim", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
            rb.gravityScale = 0.5f; // Réduire la gravité pour simuler la flottabilité
            Debug.Log("Entered water, swimming enabled.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            rb.gravityScale = 1f; // Restaurer la gravité normale
            Debug.Log("Exited water, swimming disabled.");
        }
    }
}
