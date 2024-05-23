using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float smoothTime = 0.1f; // Temps de lissage pour le mouvement

    public Collider2D colliderToFreeze; // Collider dont le Rigidbody doit devenir statique

    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Récupérer l'entrée horizontale (gauche/droite)
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    void FixedUpdate()
    {
        // Mouvement lissé
        Vector3 targetVelocity = new Vector2(horizontalMove, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, smoothTime);

        // Vérifie si le script est activé et que le collider est défini
        if (enabled && colliderToFreeze != null)
        {
            // Rend le Rigidbody du collider statique
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                colliderRb.isKinematic = true;
            }
        }
    }

    void OnDisable()
    {
        // Lorsque le script est désactivé, réactive la physique du Rigidbody du collider si nécessaire
        if (colliderToFreeze != null)
        {
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                colliderRb.isKinematic = false;
            }
        }
    }
}
