using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public Transform pointA; // Le point de départ
    public Transform pointB; // Le point d'arrivée
    public float speed = 2f; // Vitesse de déplacement
    public float waitTime = 2f; // Temps d'attente en secondes

    private bool movingToB = true;

    void Start()
    {
        // Initialiser la position de l'objet au point A
        transform.position = pointA.position;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector3 target = movingToB ? pointB.position : pointA.position;

            // Boucle jusqu'à ce que l'objet atteigne la position cible
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            // S'assurer que la position finale est exactement la position cible
            transform.position = target;

            // Inverser l'échelle en x chaque fois qu'un point est atteint
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            // Si l'objet est arrivé au point B, attendre
            if (movingToB)
            {
                yield return new WaitForSeconds(waitTime);
            }

            // Inverser la direction
            movingToB = !movingToB;
        }
    }
}
