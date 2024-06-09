using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool enableParabolicMovement = true;
    [SerializeField] private float parabolicHeight = 2f;
    [SerializeField] private float parabolicDuration = 1.5f;

    [Header("Collision avec le joueur")]
    public GameObject playerObject; // L'objet avec lequel la caméra doit entrer en collision
    public Vector3 newPositionAfterCollision { get; private set; } // La nouvelle position de la caméra après la collision

    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = CalculateDesiredPosition();
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    private Vector3 CalculateDesiredPosition()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);

        if (enableParabolicMovement)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            float parabolicHeightFactor = Mathf.Clamp01(distanceToTarget / parabolicDuration);
            float height = Mathf.Lerp(0, parabolicHeight, parabolicHeightFactor);
            Vector3 parabolicOffset = Vector3.up * height * Mathf.Sin(parabolicHeightFactor * Mathf.PI);
            desiredPosition.y = transform.position.y + parabolicOffset.y;
        }

        return desiredPosition;
    }

    // Méthode appelée lorsqu'un objet entre en collision avec le collider de ce GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet en collision a le tag "Player"
        {
            Debug.Log("Collision avec le joueur détectée");
            // Définir la nouvelle position de la caméra après la collision
            newPositionAfterCollision = new Vector3(transform.position.x, other.transform.position.y + offset.y, transform.position.z);
            Debug.Log("Nouvelle position de la caméra après collision : " + newPositionAfterCollision);
        }
    }
}
