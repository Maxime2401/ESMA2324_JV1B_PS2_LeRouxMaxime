using UnityEngine;
using System.Collections;

public class FullTurnObject : MonoBehaviour
{
    public float minInterval = 2f; // Intervalle minimum entre chaque tour complet
    public float maxInterval = 5f; // Intervalle maximum entre chaque tour complet

    private bool isTurning = false; // Indique si l'objet est en train d'effectuer un tour complet

    void Start()
    {
        // Démarre la routine pour effectuer des tours complets
        StartCoroutine(PerformFullTurns());
    }

    IEnumerator PerformFullTurns()
    {
        while (true)
        {
            // Attendre un intervalle aléatoire avant d'effectuer le prochain tour complet
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            // Si l'objet n'est pas déjà en train de tourner, commence un tour complet
            if (!isTurning)
            {
                StartCoroutine(TurnObject());
            }
        }
    }

    IEnumerator TurnObject()
    {
        isTurning = true;

        // Tourner l'objet de 360 degrés sur l'axe Z
        float duration = 2f; // Durée du tour complet
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Lerp(0f, 360f, elapsed / duration);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        // S'assurer que l'objet revient à sa rotation initiale après un tour complet
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        isTurning = false;
    }
}
