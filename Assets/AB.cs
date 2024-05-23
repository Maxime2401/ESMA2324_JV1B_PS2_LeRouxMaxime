using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform pointA; // Point A
    public Transform pointB; // Point B
    public float speed = 2f; // Vitesse de déplacement
    public float gravityScale = 1f; // Échelle de gravité

    private Vector3 startingPosition; // Position de départ
    private Rigidbody2D rb; // Rigidbody pour la gravité

    void Start()
    {
        startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale; // Appliquer l'échelle de gravité
    }

    void FixedUpdate()
    {
        // Calculer la position interpolée entre A et B
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 newPosition = Vector3.Lerp(pointA.position, pointB.position, t);

        // Déplacer l'objet vers la nouvelle position
        rb.MovePosition(newPosition);
    }
}
