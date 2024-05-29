using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public string sceneName; // Nom de la scène à charger
    public Slider progressBar; // Barre de progression
    public Image transitionImage; // Image de transition
    public float transitionDuration = 1f; // Durée de la transition
    public float fillSpeed = 0.5f; // Vitesse de remplissage de la barre de progression

    void Start()
    {
        // Démarrer la coroutine de chargement de scène avec transition
        StartCoroutine(LoadSceneWithTransition());
    }

    IEnumerator LoadSceneWithTransition()
    {
        // Lancer la transition inversée
        yield return StartCoroutine(StartReverseTransition());

        // Remplir la barre de progression jusqu'à 100%
        while (progressBar.value < 1f)
        {
            progressBar.value += fillSpeed * Time.deltaTime;
            yield return null;
        }

        // Lancer la transition normale
        StartCoroutine(StartTransition());

        // Attendre une seconde avant de charger la scène suivante
        yield return new WaitForSeconds(1f);

        // Charger la scène suivante
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator StartTransition()
    {
        // Activer l'image de transition
        transitionImage.gameObject.SetActive(true);

        // Faire progressivement apparaître l'image de transition
        float timer = 0f;
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / transitionDuration);
            SetImageAlpha(transitionImage, alpha);
            yield return null;
        }
    }

    IEnumerator StartReverseTransition()
    {
        // Activer l'image de transition
        transitionImage.gameObject.SetActive(true);

        // Faire disparaître l'image de transition
        float timer = 0f;
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / transitionDuration); // Disparaître progressivement
            SetImageAlpha(transitionImage, alpha);
            yield return null;
        }

        // Désactiver l'image de transition
        transitionImage.gameObject.SetActive(false);
    }

    // Fonction pour régler l'alpha d'une image
    void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
