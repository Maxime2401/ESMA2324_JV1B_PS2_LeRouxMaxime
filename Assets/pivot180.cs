using UnityEngine;
using System.Collections;

public class HalfTurnObject : MonoBehaviour
{
    public float minInterval = 2f; // Intervalle minimum entre chaque demi-tour
    public float maxInterval = 5f; // Intervalle maximum entre chaque demi-tour

    private bool isTurning = false; // Indique si l'objet est en train d'effectuer un demi-tour

    void Start()
    {
        // Démarre la routine pour effectuer des demi-tours
        StartCoroutine(PerformHalfTurns());
    }

    IEnumerator PerformHalfTurns()
    {
        while (true)
        {
            // Attendre un intervalle aléatoire avant d'effectuer le prochain demi-tour
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            // Si l'objet n'est pas déjà en train de tourner, commence un demi-tour
            if (!isTurning)
            {
                StartCoroutine(TurnObject());
            }
        }
    }

    IEnumerator TurnObject()
    {
        isTurning = true;

        // Tourner l'objet de 180 degrés sur l'axe Z
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 0f, 180f);
        float duration = 0.5f; // Durée du demi-tour
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            yield return null;
        }

        // Mettre à jour la rotation finale pour éviter les erreurs d'arrondi
        transform.rotation = endRotation;

        isTurning = false;
    }
}
