using UnityEngine;
using System.Collections;

public class ObjectFollow : MonoBehaviour
{
    public Transform target; // Référence au joueur à suivre
    public float followSpeed = 5f; // Vitesse de suivi du joueur
    public float stationaryDuration = 1f; // Durée pendant laquelle l'objet reste statique après que le joueur soit sorti de la collision
    public float returnSmoothness = 5f; // Lissage du retour à la position initiale

    private bool isFollowing = false; // Indique si le suivi est activé
    private Vector3 initialPosition; // Position initiale de l'objet
    public MonoBehaviour scriptToDisable; // Le script à désactiver lors du suivi
    public MonoBehaviour scriptToapa; // Le script à désactiver lors 
    public Collider2D followArea; // La zone à l'intérieur de laquelle l'objet peut suivre le joueur

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isFollowing && target != null)
        {
            // Calculer la direction vers la cible en ignorant la composante Z
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.z = 0f; // Ignorer la composante Z
            // Déplacer l'objet vers la cible avec une vitesse constante
            transform.position += directionToTarget.normalized * followSpeed * Time.deltaTime;

            // Limiter la position de l'objet à l'intérieur de la zone de suivi
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, followArea.bounds.min.x, followArea.bounds.max.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, followArea.bounds.min.y, followArea.bounds.max.y);
            transform.position = clampedPosition;
        }
    }

    public void StartFollowing(Transform newTarget)
    {
        // Activer le suivi avec la nouvelle cible
        target = newTarget;
        isFollowing = true;

        // Désactiver le script si spécifié
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
        
        if (scriptToapa != null)
        {
            scriptToapa.enabled = false;
        }
    }

    public void StopFollowing()
    {
        // Désactiver le suivi
        isFollowing = false;
        target = null;

        // Activer le script si spécifié
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = true;
        }
        if (scriptToapa != null)
        {
            scriptToapa.enabled = true;
        }
        // Lancer la coroutine pour revenir à la position initiale en douceur
        StartCoroutine(ReturnToInitialPositionSmooth());
    }

    IEnumerator ReturnToInitialPositionSmooth()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < stationaryDuration)
        {
            transform.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / stationaryDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurer que l'objet revient exactement à sa position initiale à la fin de la durée
        transform.position = initialPosition;
    }
}
