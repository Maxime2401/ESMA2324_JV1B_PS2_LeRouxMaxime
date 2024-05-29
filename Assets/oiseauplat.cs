using UnityEngine;
using System.Collections;

public class BirdMovementplat : MonoBehaviour
{
    public Transform pointA; // Point A
    public Transform pointB; // Point B
    public float speed = 2f; // Vitesse de déplacement entre A et B
    public float disappearTime = 3f;
    public float disappertriger = 2f; // Temps avant que la plateforme ne disparaisse
    public float triggerDuration = 1f; // Durée pendant laquelle le collider est un trigger
    public float fallGravityScale = -4f; // Valeur de la gravité lors de la chute
    public float contactSpeed = 5f; // Vitesse lorsqu'il entre en contact avec le joueur

    private Rigidbody2D rb;
    private bool movingToB = true;
    private bool isFalling = false;
    private bool isTriggered = false; // État du collider
    private Vector3 initialPosition; // Position initiale de la plateforme
    private Collider2D col; // Référence au collider de l'objet

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Désactiver la gravité au début
        initialPosition = transform.position; // Enregistrer la position initiale
        col = GetComponent<Collider2D>(); // Obtenir le collider de l'objet
    }

    void Update()
    {
        if (!isFalling)
        {
            MoveBetweenPoints();
        }
    }

    void MoveBetweenPoints()
    {
        // Calculer la position cible
        Vector3 targetPosition = movingToB ? pointB.position : pointA.position;
        // Déplacer la plateforme vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Inverser la direction lorsque la plateforme atteint l'un des points
        if (transform.position == targetPosition)
        {
            movingToB = !movingToB;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartFalling();
            rb.velocity = new Vector2(contactSpeed, rb.velocity.y); // Appliquer une vitesse vers la droite
            StartCoroutine(ActivateTriggerAfterDelay());
            StartCoroutine(DisappearAndReappear());
        }
    }

    void StartFalling()
    {
        isFalling = true;
        rb.gravityScale = fallGravityScale; // Activer la gravité avec la valeur publique
        rb.velocity = Vector2.zero; // Réinitialiser la vitesse actuelle
    }

    IEnumerator ActivateTriggerAfterDelay()
    {
        yield return new WaitForSeconds(disappertriger); // Attendre avant d'activer le trigger
        col.isTrigger = true; // Activer le collider comme trigger
        isTriggered = true; // Mettre à jour l'état du collider
        yield return new WaitForSeconds(triggerDuration); // Attendre pendant que le collider est un trigger
        col.isTrigger = false; // Désactiver le collider comme trigger
        isTriggered = false; // Mettre à jour l'état du collider
    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(disappearTime); // Attendre avant de disparaître

        // Désactiver la plateforme
        gameObject.SetActive(false);

        // Réactiver la plateforme et la remettre à sa position initiale
        transform.position = initialPosition;
        gameObject.SetActive(true);

        // Réinitialiser les états
        rb.gravityScale = 0; // Désactiver la gravité
        rb.velocity = Vector2.zero; // Réinitialiser la vitesse actuelle
        isFalling = false; // Réinitialiser l'état de chute
        movingToB = true; // Réinitialiser la direction du mouvement si nécessaire
    }
}
