using UnityEngine;

public class ObjectMovementChecker : MonoBehaviour
{
    public Animator animator; // Référence à l'Animator
    private Vector3 previousPosition; // Position précédente de l'objet

    void Start()
    {
        // Initialisation de la position précédente à la position actuelle au démarrage
        previousPosition = transform.position;
    }

    void Update()
    {
        // Vérifier si la position en X a changé
        if (transform.position.x != previousPosition.x)
        {
            // Si l'objet a bougé en X, mettre le paramètre 'Wake' à true
            animator.SetBool("Wake", true);
        }
        else
        {
            // Si l'objet n'a pas bougé en X, mettre le paramètre 'Wake' à false
            animator.SetBool("Wake", false);
        }

        // Mettre à jour la position précédente
        previousPosition = transform.position;
    }
}
