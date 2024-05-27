using UnityEngine;

public class Nage : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement normale du personnage
    public float jumpForce = 10f; // Force de saut du personnage
    public Animator animator; // Référence à l'Animator
    public KeyCode moveLeftKey = KeyCode.LeftArrow; // Touche pour déplacer le personnage vers la gauche
    public KeyCode moveRightKey = KeyCode.RightArrow; // Touche pour déplacer le personnage vers la droite
    public KeyCode jumpKey = KeyCode.Space; // Touche pour sauter

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialisation du composant Rigidbody2D
    }

    void Update()
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

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Saut
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Mise à jour des animations
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
        if (moveInput != 0)
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(moveInput, 1f, 1f); // Ajuste l'échelle du personnage pour qu'il regarde dans la bonne direction
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si le personnage touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Vérifie si le personnage n'est plus en contact avec le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
