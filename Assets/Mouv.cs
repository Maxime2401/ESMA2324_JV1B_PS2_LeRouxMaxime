using System.Collections;
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

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isChargingJump = false; // Indique si le saut est en cours de chargement
    private bool hasJumped = false; // Indique si le joueur a déjà sauté dans l'air
    private float jumpTime = 0f; // Temps écoulé depuis que la touche de saut est enfoncée

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialisation du composant Rigidbody2D
    }

    void Update()
    {
        // Vérifie si le personnage touche le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f);

        // Réinitialise la vitesse si le personnage touche le sol ou si le joueur relâche la touche d'espace
        if (isGrounded || (isChargingJump && Input.GetKeyUp(KeyCode.Space)))
        {
            moveSpeed = isChargingJump ? chargingMoveSpeed : moveSpeed;
        }

        // Gestion du saut
        if (isGrounded)
        {
            if (!isChargingJump)
            {
                // Si la touche de saut est enfoncée, commence à charger le saut
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isChargingJump = true;
                    jumpTime = 0f; // Réinitialise le temps de saut lorsque la touche est pressée
                }
            }
            else
            {
                // Si la touche de saut est maintenue enfoncée, continue à charger le saut
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpTime += Time.deltaTime;
                    // Si le joueur charge le saut, ajuste la vitesse de déplacement
                    moveSpeed = chargingMoveSpeed;
                }

                // Si la touche de saut est relâchée, effectue un saut
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Jump();
                }
            }
        }

        // Déplacement horizontal
        float moveSpeedAdjusted = isGrounded ? moveSpeed : airMoveSpeed; // Change la vitesse de déplacement en l'air si le joueur a déjà sauté
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeedAdjusted, rb.velocity.y);
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
        if (isGrounded)
        {
            moveSpeed = 4f;
            Debug.Log("La vitesse du personnage après le saut au sol : " + moveSpeed);
        }
        else
        {
            moveSpeed = airMoveSpeed;
            Debug.Log("La vitesse du personnage après le saut dans les airs : " + moveSpeed);
        }

        Debug.Log("Le saut est effectué. La vitesse du personnage après le saut : " + moveSpeed);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        // Réinitialiser l'état de saut lorsque le joueur touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
        }
    }
}
