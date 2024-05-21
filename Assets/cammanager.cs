using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Le transform du personnage que la caméra doit suivre
    [SerializeField] private float smoothSpeed = 0.125f; // La vitesse de suivi de la caméra
    [SerializeField] private Vector3 offset; // L'offset de position par rapport au personnage

    [Header("Parabolic Movement")]
    [SerializeField] private bool enableParabolicMovement = true; // Activer le mouvement parabolique de la caméra
    [SerializeField] private float parabolicHeight = 2f; // Hauteur maximale de la trajectoire parabolique
    [SerializeField] private float parabolicDuration = 1.5f; // Durée de la trajectoire parabolique

    private Vector3 initialPosition; // Position initiale de la caméra

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
        Vector3 desiredPosition = target.position + offset;

        if (enableParabolicMovement)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            float parabolicHeightFactor = Mathf.Clamp01(distanceToTarget / parabolicDuration);
            float height = Mathf.Lerp(0, parabolicHeight, parabolicHeightFactor);

            Vector3 directionToTarget = (target.position - initialPosition).normalized;
            Vector3 parabolicOffset = Vector3.up * height * Mathf.Sin(parabolicHeightFactor * Mathf.PI);

            desiredPosition += parabolicOffset;
        }

        return desiredPosition;
    }
}
