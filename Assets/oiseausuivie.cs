using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public float movementSpeed = 5f; // Vitesse de déplacement de l'oiseau
    public float followDuration = 5f; // Durée pendant laquelle l'oiseau suit le joueur après avoir rencontré un obstacle
    public GameObject specialObject; // Objet spécial à vérifier

    private bool isFollowing = false; // Indique si l'oiseau suit le joueur
    private float followTimer = 0f; // Compteur pour la durée de suivi
    private bool isSpecialObjectTouchingPlayer = false; // Indique si l'objet spécial touche le joueur

    void Start()
    {
        isFollowing = false;
        followTimer = 0f;
    }

    void Update()
    {
        if (isFollowing && !isSpecialObjectTouchingPlayer)
        {
            FlyTowardsPlayer();
            followTimer += Time.deltaTime;

            // Arrête de suivre le joueur une fois que la durée de suivi est écoulée
            if (followTimer >= followDuration)
            {
                isFollowing = false;
                followTimer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isFollowing = true; // Active le suivi du joueur lorsque l'oiseau entre en collision avec un obstacle
        }
        else if (other.CompareTag("Player"))
        {
            isFollowing = false; // Arrête le suivi du joueur lorsque l'oiseau entre en collision avec le joueur
        }
        else if (other.gameObject == specialObject)
        {
            isSpecialObjectTouchingPlayer = true; // Marque que l'objet spécial touche le joueur
            isFollowing = false; // Arrête le suivi du joueur lorsque l'objet spécial touche le joueur
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == specialObject)
        {
            isSpecialObjectTouchingPlayer = false; // Marque que l'objet spécial ne touche plus le joueur
        }
    }

    void FlyTowardsPlayer()
    {
        // Calcule la direction vers laquelle l'oiseau doit se déplacer (vers le joueur)
        Vector2 direction = (player.position - transform.position).normalized;

        // Déplace l'oiseau vers le joueur selon l'axe vertical (axe Y) à la vitesse spécifiée
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
    }
}
