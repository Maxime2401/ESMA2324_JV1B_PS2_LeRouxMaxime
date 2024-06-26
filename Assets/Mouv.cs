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

    public AudioClip jumpSound; // Son du saut
    public AudioClip moveSound; // Son du déplacement
    public AudioClip landingSound; // Son de l'atterrissage
    public AudioClip chargingSound; // Son de chargement du saut
    private AudioSource audioSource; // Référence à l'AudioSource pour jouer les sons

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
        rb = GetComponent<Rigidbody2D>();
        keyBindingsManager = FindObjectOfType<KeyBindingsManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (groundCheck == null)
        {
            Debug.LogError("groundCheck n'est pas assigné.");
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D n'est pas assigné.");
        }
        if (animator == null)
        {
            Debug.LogError("Animator n'est pas assigné.");
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer n'est pas assigné.");
        }
        if (keyBindingsManager == null)
        {
            Debug.LogError("KeyBindingsManager n'est pas trouvé dans la scène.");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource n'est pas trouvé sur ce GameObject.");
        }
    }

    void Update()
    {
        if (keyBindingsManager == null)
        {
            return;
        }

        // Vérifie si le personnage touche le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f);

        float verticalMovement = transform.position.y - previousYPosition;
        previousYPosition = transform.position.y;

        // Mise à jour des booléens d'ascension et de descente
        isAscending = verticalMovement > 0;
        isDescending = verticalMovement < 0;

        if (isDescending)
        {
            if (animator != null)
            {
                animator.SetBool("Fall", true);
            }
        }

        if (animator != null)
        {
            animator.SetBool("Up", isAscending);
        }

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

        if (isGrounded)
        {
            if (moveInput != 0)
            {
                if (animator != null)
                {
                    animator.SetBool("Run", true);
                }
                spriteRenderer.flipX = moveInput < 0;
                // Jouer le son de déplacement
                if (audioSource != null && moveSound != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(moveSound);
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("Run", false);
                }
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("Run", false);
            }
        }

        // Gestion du saut
        if (isGrounded)
        {
            if (!isChargingJump)
            {
                if (Input.GetKeyDown(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    isChargingJump = true;
                    jumpTime = 0f;

                    // Jouer le son de chargement du saut
                    if (audioSource != null && chargingSound != null)
                    {
                        audioSource.clip = chargingSound;
                        audioSource.Play();
                    }
                }
            }
            else
            {
                if (Input.GetKey(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    jumpTime += Time.deltaTime;
                    if (animator != null)
                    {
                        animator.SetBool("Charge", true);
                    }
                    moveSpeed = chargingMoveSpeed;
                }

                if (Input.GetKeyUp(keyBindingsManager.GetKeyCodeForAction("Jump")))
                {
                    if (animator != null)
                    {
                        animator.SetBool("Charge", false);
                    }
                    audioSource.Stop();
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        if (hasJumped)
        {
            return;
        }

        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, Mathf.Clamp01(jumpTime / jumpTimeThreshold));
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Jouer le son de saut
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }

        isChargingJump = false;
        hasJumped = true;
        jumpTime = 0f;

        moveSpeed = isGrounded ? 4f : airMoveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            if (animator != null)
            {
                animator.SetBool("Fall", false);
            }
            if (animator != null)
            {
                animator.SetBool("Sol", true);
            }

            // Jouer le son de l'atterrissage
            if (audioSource != null && landingSound != null)
            {
                audioSource.PlayOneShot(landingSound);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (animator != null)
            {
                animator.SetBool("Sol", false);
            }
        }
    }
}
