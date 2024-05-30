using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform pointA; // Point A
    public Transform pointB; // Point B
    public float speed = 2f; // Vitesse de déplacement
    public float gravityScale = 1f; // Échelle de gravité

    private Vector3 previousPosition; // Position précédente de la plateforme
    private Rigidbody2D rb; // Rigidbody pour la gravité
    private Transform playerTransform; // Transform du joueur
    private Animator playerAnimator; // Animator du joueur

    void Start()
    {
        previousPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale; // Appliquer l'échelle de gravité
    }

    void FixedUpdate()
    {
        // Calculer la position interpolée entre A et B
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 newPosition = Vector3.Lerp(pointA.position, pointB.position, t);

        // Calculer le déplacement de la plateforme
        Vector3 deltaPosition = newPosition - previousPosition;

        // Déplacer l'objet vers la nouvelle position
        rb.MovePosition(newPosition);

        // Si le joueur est sur la plateforme, déplacer le joueur avec la plateforme
        if (playerTransform != null)
        {
            playerTransform.position += deltaPosition;
        }

        // Mettre à jour la position précédente de la plateforme
        previousPosition = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = collision.transform; // Stocker le transform du joueur
            playerAnimator = collision.gameObject.GetComponent<Animator>(); // Récupérer l'Animator du joueur

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Fall", false); // Désactiver l'animation de chute
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Fall", true); // Activer l'animation de chute
            }

            playerTransform = null; // Réinitialiser le transform du joueur
            playerAnimator = null; // Réinitialiser l'Animator du joueur
        }
    }
}
