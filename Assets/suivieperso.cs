using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // Le transform du joueur que l'objet doit suivre
    public float smoothSpeed = 0.125f; // La vitesse de déplacement de l'objet vers le joueur

    private Vector3 velocity = Vector3.zero; // La vitesse actuelle de l'objet

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculer la position cible à laquelle l'objet doit se déplacer
            Vector3 targetPosition = target.position;

            // Interpoler progressivement la position actuelle de l'objet vers la position cible
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

            // Mettre à jour la position de l'objet
            transform.position = smoothPosition;
        }
    }
}
