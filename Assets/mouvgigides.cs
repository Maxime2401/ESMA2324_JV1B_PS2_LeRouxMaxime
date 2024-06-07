using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float smoothTime = 0.1f; // Temps de lissage pour le mouvement

    public Collider2D colliderToFreeze; // Collider dont le Rigidbody doit devenir statique

    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private Vector3 velocity = Vector3.zero;
    private KeyBindingsManager keyBindingsManager; // Référence au KeyBindingsManager

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        keyBindingsManager = FindObjectOfType<KeyBindingsManager>();
    }

    void Update()
    {
        if (keyBindingsManager == null)
        {
            return;
        }

        // Récupérer les touches assignées pour les mouvements gauche et droite
        KeyCode moveLeftKey = keyBindingsManager.GetKeyCodeForAction("MoveLeft");
        KeyCode moveRightKey = keyBindingsManager.GetKeyCodeForAction("MoveRight");

        // Initialiser horizontalMove à 0
        horizontalMove = 0f;

        // Vérifier si les touches sont enfoncées et ajuster horizontalMove en conséquence
        if (moveLeftKey != KeyCode.None && Input.GetKey(moveLeftKey))
        {
            horizontalMove = -moveSpeed;
        }
        else if (moveRightKey != KeyCode.None && Input.GetKey(moveRightKey))
        {
            horizontalMove = moveSpeed;
        }
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
