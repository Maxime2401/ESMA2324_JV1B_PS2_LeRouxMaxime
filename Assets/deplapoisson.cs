using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    public float swimSpeed = 1f; // Vitesse de nage du poisson
    public float rotationSpeed = 2f; // Vitesse de rotation du poisson
    public float changeDirectionInterval = 2f; // Intervalle de temps entre chaque changement de direction
    public Transform swimArea; // Zone de nage autorisée pour le poisson

    private Rigidbody2D rb;
    private Vector2 swimDirection;

    private bool isReversingDirection = false; // Indique si le poisson est en train de faire demi-tour
    private float reverseDirectionDelay = 1.0f; // Délai entre chaque inversion de direction
    private float lastReverseTime; // Temps du dernier demi-tour

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Démarre la routine pour changer de direction de nage
        StartCoroutine(ChangeSwimDirection());
    }

    void Update()
    {
        // Faire nager le poisson dans la direction actuelle
        rb.velocity = swimDirection * swimSpeed;

        // Tourner le poisson dans la direction de nage
        float angle = Mathf.Atan2(swimDirection.y, swimDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ChangeSwimDirection()
    {
        while (true)
        {
            // Générer un angle aléatoire entre 0 et 35 degrés
            float randomAngle = Random.Range(0f, 35f);
            // Convertir l'angle en radians
            float radianAngle = randomAngle * Mathf.Deg2Rad;
            // Calculer la direction en utilisant l'angle
            swimDirection = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));

            // Attendre un certain temps avant de changer à nouveau de direction
            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary") )
        {
            // Le poisson est en dehors de la zone de nage, faire demi-tour
            swimDirection *= -1f;
            isReversingDirection = true;
            lastReverseTime = Time.time;
            StartCoroutine(ResetReverseDirection());
        }
    }

    IEnumerator ResetReverseDirection()
    {
        // Attendre un certain temps avant de réinitialiser l'état de demi-tour
        yield return new WaitForSeconds(reverseDirectionDelay);
        isReversingDirection = false;
    }
}
