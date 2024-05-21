using UnityEngine;

public class Mountmovements : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public float movementSpeed = 5f; // Vitesse de déplacement de la monture
    public float followDuration = 5f; // Durée pendant laquelle la monture suit le joueur après avoir rencontré un obstacle

    private bool isFollowing = false; // Indique si la monture suit le joueur
    private float followTimer = 0f; // Compteur pour la durée de suivi

    void Start()
    {
        isFollowing = false;
        followTimer = 0f;
    }

    void Update()
    {
        if (isFollowing)
        {
            MoveTowardsPlayer();
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
            isFollowing = true; // Active le suivi du joueur lorsque la monture entre en collision avec un obstacle
        }
        else if (other.CompareTag("Player"))
        {
            isFollowing = false; // Arrête le suivi du joueur lorsque la monture entre en collision avec le joueur
        }
    }

    void MoveTowardsPlayer()
    {
        // Calcule la direction vers laquelle la monture doit se déplacer (vers le joueur)
        Vector2 direction = (player.position - transform.position).normalized;

        // Déplace la monture dans cette direction à la vitesse spécifiée
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
