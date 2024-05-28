using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    // Déclaration des variables pour les points de destination et les paramètres de mouvement
    public GameObject[] targetPoints; // Tableau des points de destination en tant que GameObject
    public float speed = 5f; // Vitesse de déplacement du personnage
    public float stoppingDistance = 0.1f; // Distance seuil pour considérer que le personnage a atteint un point

    private int currentTargetIndex = 0; // Index du point de destination actuel
    private bool isMoving = true; // Indique si le personnage est en mouvement ou non

    void Update()
    {
        // Si le personnage est en mouvement, déplacer vers le point de destination actuel
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // Vérifier s'il y a des points de destination
        if (targetPoints.Length == 0)
        {
            Debug.LogError("No target points found.");
            return;
        }

        // Calculer la direction vers le point de destination
        Vector3 direction = targetPoints[currentTargetIndex].transform.position - transform.position;

        // Si le personnage n'a pas atteint le point de destination
        if (direction.magnitude > stoppingDistance)
        {
            // Déplacer le personnage vers le point de destination en utilisant la vitesse et le temps écoulé depuis la dernière frame
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Arrêter le personnage car il est assez proche du point de destination
            isMoving = false;
        }
    }
}
