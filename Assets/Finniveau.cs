using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    // GameObject pour la collision spécifique
    public GameObject specialObject;
    // GameObject contenant les scripts à désactiver sur le joueur
    public float moveSpeed = 5f;
    // Durée pendant laquelle le joueur est bloqué (en secondes)
    public float blockDuration = 1f;
    // Durée pendant laquelle le joueur se déplace vers la droite (en secondes)
    public float moveDuration = 2f;

    // Scripts à désactiver sur le joueur
    public MonoBehaviour[] scriptsToDisable;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Le joueur se déplace vers la droite pendant la durée spécifiée
        if (blockDuration + moveDuration <= Time.time)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si le joueur entre en collision avec le GameObject spécifique
        if (other.gameObject == specialObject)
        {
            // Désactiver les scripts spécifiés sur le joueur
            foreach (var script in scriptsToDisable)
            {
                script.enabled = false;
            }

            // Bloquer le joueur pendant la durée spécifiée
            rb.velocity = Vector2.zero;
            Invoke("ActivateScriptsAndMove", blockDuration + moveDuration);
        }
    }

    // Fonction pour réactiver les scripts sur le joueur après la durée spécifiée
    void ActivateScriptsAndMove()
    {
        // Réactiver les scripts spécifiés sur le joueur
        foreach (var script in scriptsToDisable)
        {
            script.enabled = true;
        }
    }
}
