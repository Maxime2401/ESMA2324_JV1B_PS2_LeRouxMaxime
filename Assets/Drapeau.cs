using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangerFinniveau : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la scène à charger
    public Image transitionCircle; // Référence à l'image de transition
    public float transitionDuration = 1f; // Durée de la transition

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeSceneAfterDelay(3f)); // Délai de 3 secondes
            StartCoroutine(ChangeSceneAfterDelay(3f)); // Délai de 3 secondes avant de changer de scène
        }
    }

    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        // Démarrer la transition au début de la coroutine
        StartCoroutine(StartTransition());

        yield return new WaitForSeconds(delay);

        // Charger la nouvelle scène
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator StartTransition()
    {
        if (transitionCircle != null)
        {
            Debug.Log("Starting transition...");

            // Activer l'image de transition
            transitionCircle.gameObject.SetActive(true);

            // Faire progressivement apparaître l'image de transition
            yield return StartCoroutine(ScaleTransitionCircle(Vector3.one, transitionDuration));

            Debug.Log("Transition completed!");
        }
    }

    IEnumerator ScaleTransitionCircle(Vector3 targetScale, float duration)
    {
        if (transitionCircle == null)
        {
            yield break;
        }

        Vector3 startScale = transitionCircle.rectTransform.localScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            transitionCircle.rectTransform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            yield return null;
        }

        transitionCircle.rectTransform.localScale = targetScale;
    }
}