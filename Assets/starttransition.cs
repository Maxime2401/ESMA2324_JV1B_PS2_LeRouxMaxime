using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CircularTransition : MonoBehaviour
{
    public Image transitionCircle; // Référence à l'image circulaire pour la transition
    public float transitionDuration = 1f; // Durée de la transition

    void Start()
    {
        Debug.Log("CircularTransition: Start()");
        StartCoroutine(ScaleTransitionCircle(Vector3.zero, transitionDuration)); // Taille cible est maintenant Vector3.zero
    }

    IEnumerator ScaleTransitionCircle(Vector3 targetScale, float duration)
    {
        Debug.Log("CircularTransition: ScaleTransitionCircle() - Target Scale: " + targetScale + ", Duration: " + duration);
        if (transitionCircle == null)
        {
            Debug.LogWarning("CircularTransition: ScaleTransitionCircle() - Transition circle reference is null!");
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
        Debug.Log("CircularTransition: ScaleTransitionCircle() - Transition completed!");
    }
}
