using UnityEngine;
using System.Collections;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform pointA; // Premier point
    public Transform pointB; // Deuxième point (où il fera une pause)
    public Transform pointC; // Troisième point
    public float vitesse = 2f; // Vitesse de déplacement
    public float pauseDuree = 2f; // Durée de la pause au point B

    private bool estActif = false; // État pour vérifier si l'objet a été activé

    void OnEnable()
    {
        if (!estActif)
        {
            // Téléporter l'objet à pointA
            transform.position = pointA.position;
            
            // Démarrer le cycle de déplacement lorsque l'objet est activé
            StartCoroutine(DeplacementCycle());
            estActif = true;
        }
    }

    IEnumerator DeplacementCycle()
    {
        // Déplacement du point A au point B
        yield return StartCoroutine(DeplacerVersPoint(pointA.position, pointB.position));

        // Pause au point B
        yield return new WaitForSeconds(pauseDuree);

        // Déplacement du point B au point C
        yield return StartCoroutine(DeplacerVersPoint(pointB.position, pointC.position));

        // Le script termine ici ; il n'y a pas de retour à A
    }

    IEnumerator DeplacerVersPoint(Vector3 origine, Vector3 destination)
    {
        // Calculer la distance entre la position actuelle et la destination
        float distance = Vector3.Distance(origine, destination);

        while (distance > 0.1f) // Continuer tant que l'objet n'est pas assez proche de la destination
        {
            // Déplacer l'objet vers la destination
            transform.position = Vector3.MoveTowards(transform.position, destination, vitesse * Time.deltaTime);

            // Recalculer la distance à chaque frame
            distance = Vector3.Distance(transform.position, destination);

            // Attendre la fin du frame
            yield return null;
        }

        // S'assurer que l'objet atteint exactement la destination
        transform.position = destination;
    }
}
