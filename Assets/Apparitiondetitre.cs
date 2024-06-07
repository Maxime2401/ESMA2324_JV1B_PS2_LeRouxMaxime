using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDelay : MonoBehaviour
{
    // Référence à l'objet UI que vous voulez afficher
    public GameObject uiElement;
    public float fadeDuration = 1f; // Durée du fondu en secondes

    // Référence au bouton à activer
    public Button button;

    private CanvasGroup canvasGroup;

    void Start()
    {
        if (uiElement != null)
        {
            canvasGroup = uiElement.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = uiElement.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f; // Commencez avec l'alpha à 0
            uiElement.SetActive(true); // Assurez-vous que l'élément UI est actif

            if (button != null)
            {
                button.gameObject.SetActive(false); // Assurez-vous que le bouton est désactivé au départ
            }

            // Démarrez la coroutine après un délai de 14 secondes
            StartCoroutine(ShowUIAfterDelay(14f));
        }
        else
        {
            Debug.LogError("UI Element n'est pas assigné dans l'inspecteur !");
        }
    }

    IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f; // Assurez-vous que l'alpha est bien à 1 à la fin

        if (button != null)
        {
            button.gameObject.SetActive(true); // Activez le bouton une fois que le fondu est terminé
        }
    }
}
