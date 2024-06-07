using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class activemerci : MonoBehaviour
{
    public GameObject shopUI; // Référence à l'interface utilisateur du shop
    public Image[] otherImages; // Références aux Images UI des autres images
    public float smoothTime = 0.5f; // Temps de transition en secondes
    public float delayBeforeActivation = 2f; // Délai avant l'activation de l'interface utilisateur

    private bool isPlayerInside = false; // Indicateur si le joueur est à l'intérieur du shop
    private float[] targetAlphas; // Alphas cibles pour les images UI
    private float[] currentVelocities; // Vitesses courantes pour le lerp

    void Start()
    {
        // S'assurer que les images UI sont valides
        if (otherImages == null || otherImages.Length == 0)
        {
            Debug.LogError("Les Images UI ne sont pas assignées au script !");
            enabled = false; // Désactiver le script s'il n'est pas configuré correctement
            return;
        }

        targetAlphas = new float[otherImages.Length];
        currentVelocities = new float[otherImages.Length];

        // Initialiser les alphas cibles au début (0 si le joueur n'est pas dans la zone, 1 sinon)
        for (int i = 0; i < otherImages.Length; i++)
        {
            targetAlphas[i] = isPlayerInside ? 1f : 0f;
            // Définir l'alpha initial des images UI
            Color imageColor = otherImages[i].color;
            imageColor.a = targetAlphas[i];
            otherImages[i].color = imageColor;
        }
    }

    void Update()
    {
        // Lerp des alphas actuels vers les alphas cibles pour chaque image
        for (int i = 0; i < otherImages.Length; i++)
        {
            float newAlpha = Mathf.SmoothDamp(otherImages[i].color.a, targetAlphas[i], ref currentVelocities[i], smoothTime);
            // Mettre à jour l'alpha de chaque image UI
            Color newColor = otherImages[i].color;
            newColor.a = newAlpha;
            otherImages[i].color = newColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est à l'intérieur de la zone du shop
            isPlayerInside = true;
            // Mettre à jour les alphas cibles à 1 pour montrer les images UI
            for (int i = 0; i < otherImages.Length; i++)
            {
                targetAlphas[i] = 1f;
            }
            // Démarrer la coroutine pour activer l'interface utilisateur après un certain délai
            StartCoroutine(ActivateShopUIAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Arrêter la coroutine si le joueur quitte la zone du shop
            StopCoroutine(ActivateShopUIAfterDelay());
            // Réinitialiser l'indicateur de joueur à l'extérieur de la zone du shop
            isPlayerInside = false;
            // Mettre à jour les alphas cibles à 0 pour cacher les images UI
            for (int i = 0; i < otherImages.Length; i++)
            {
                targetAlphas[i] = 0f;
            }

            // Désactiver l'interface utilisateur du shop
            if (shopUI != null)
            {
                shopUI.SetActive(false);
            }
        }
    }

    IEnumerator ActivateShopUIAfterDelay()
    {
        // Attendre pendant le délai spécifié
        yield return new WaitForSeconds(delayBeforeActivation);

        // Activer l'interface utilisateur du shop
        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }
    }
}
