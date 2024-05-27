using UnityEngine;

public class BasicCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement normale du personnage
    public float airMoveSpeed = 2f; // Vitesse de déplacement dans l'air
    public float chargingMoveSpeed = 1f; // Vitesse de déplacement pendant la charge du saut
    public float minJumpForce = 10f; // Force de saut minimale du personnage
    public float maxJumpForce = 20f; // Force de saut maximale du personnage
    public float jumpTimeThreshold = 0.5f; // Durée minimale de maintien de la touche de saut pour un saut maximal
    public Transform groundCheck; // Transform de vérification du sol
    public Collider2D captGauche;
    public Collider2D captDroit;

    private Rigidbody2D rb;
    private bool isGrounded;
    public bool droite = true;
    public bool gauche = true;
    private bool isChargingJump = false; // Indique si le saut est en cours de chargement
    private bool hasJumped = false; // Indique si le joueur a déjà sauté dans l'air
    private float jumpTime = 0f; // Temps écoulé depuis que la touche de saut est enfoncée

    private KeyBindingsManager keyBindingsManager; // Référence au gestionnaire de touches
    private Animator animator; // Référence à l'Animator
    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    private bool isAscending = false; // Indique si le personnage est en train de monter
    private bool isDescending = false; // Indique si le personnage est en train de descendre
    private float previousYPosition; // Position Y précédente du personnage


    void Start()
    {
        previousYPosition = transform.position.y;
        rb = GetComponent<Rigidbody2D>(); // Initialisation du composant Rigidbody2D
        keyBindingsManager = FindObjectOfType<KeyBindingsManager>(); // Trouver le gestionnaire de touches dans la scène
        animator = GetComponent<Animator>(); // Initialisation de l'Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialisation du SpriteRenderer
    }

    void Update()
    {


        // Vérifie si le personnage touche le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f);

        float verticalMovement = transform.position.y - previousYPosition;

        // Mise à jour de la position Y précédente
        previousYPosition = transform.position.y;

        // Mise à jour des booléens d'ascension et de descente
        isAscending = verticalMovement > 0;
        if (isDescending = verticalMovement < 0)
        {
            animator.SetBool("Fall", true);
        }

        // Mettre à jour l'état de l'Animator en fonction du mouvement vertical
        animator.SetBool("Up", isAscending);
        

        // Déplacement horizontal
        float moveInput = 0f;
        if (keyBindingsManager.GetKeyCodeForAction("MoveLeft") != KeyCode.None && gauche)
        {
            if (Input.GetKey(keyBindingsManager.GetKeyCodeForAction("MoveLeft")))
            {
                moveInput -= 1f;
            }
        }
        if (keyBindingsManager.GetKeyCodeForAction("MoveRight") != KeyCode.None && droite)
        {
            if (Input.GetKey(keyBindingsManager.GetKeyCodeForAction("MoveRight")))
            {
                moveInput += 1f;
            }
        }

        // Appliquer le mouvement horizontal
        float moveSpeedAdjusted = isGrounded ? moveSpeed : airMoveSpeed;
        rb.velocity = new Vector2(moveInput * moveSpeedAdjusted, rb.velocity.y);

        // Mise à jour des animations et flip du sprite
        if (isGrounded)
        {
            if (moveInput != 0)
            {
                animator.SetBool("Run", true);
                spriteRenderer.flipX = moveInput < 0; // Flip le sprite en fonction de la direction
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Gestion du saut
        if (isGrounded)
        {
            if (!isChargingJump)
            {
                // Si la touche de saut est enfoncée, commence à charger le saut
                if (Input.GetKeyDown(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    isChargingJump = true;
                    jumpTime = 0f; // Réinitialise le temps de saut lorsque la touche est pressée
                }
            }
            else
            {
                // Si la touche de saut est maintenue enfoncée, continue à charger le saut
                if (Input.GetKey(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    jumpTime += Time.deltaTime;
                    animator.SetBool("Charge", true);
                    // Si le joueur charge le saut, ajuste la vitesse de déplacement
                    moveSpeed = chargingMoveSpeed;
                }

                // Si la touche de saut est relâchée, effectue un saut
                if (Input.GetKeyUp(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    animator.SetBool("Charge", false);
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        // Si le joueur a déjà sauté dans l'air, ne faites rien
        if (hasJumped)
        {
            Debug.Log("Le joueur a déjà sauté dans l'air.");
            return;
        }

        // Calculer la force de saut en fonction du temps de saut
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, Mathf.Clamp01(jumpTime / jumpTimeThreshold));

        // Appliquer la force de saut au personnage
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Réinitialiser les valeurs liées au saut
        isChargingJump = false;
        hasJumped = true;
        jumpTime = 0f;

        // Réinitialiser la vitesse du personnage à sa valeur normale après le saut
        moveSpeed = isGrounded ? 4f : airMoveSpeed;

        Debug.Log("Le saut est effectué. La vitesse du personnage après le saut : " + moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Réinitialiser l'état de saut lorsque le joueur touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            animator.SetBool("Fall", false);
        }

        // Vérifier les collisions avec les capteurs gauche et droit
        if (collision.collider == captGauche && collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision détectée avec le mur gauche.");
            gauche = false;
        }

        if (collision.collider == captDroit && collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision détectée avec le mur droit.");
            droite = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Réinitialiser les drapeaux lorsque les collisions cessent
        if (collision.collider == captGauche && collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision avec le mur gauche terminée.");
            gauche = true;
        }

        if (collision.collider == captDroit && collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision avec le mur droit terminée.");
            droite = true;
        }
    }
}
