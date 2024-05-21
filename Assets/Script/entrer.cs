using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{
    public Image image; // Référence à l'Image de l'UI à clignoter
    public float blinkSpeed = 1f; // Vitesse de clignotement
    public float minAlpha = 0.2f; // Opacité minimale
    public float maxAlpha = 1f; // Opacité maximale

    private bool isIncreasing = true; // Détermine si l'opacité augmente ou diminue
    private float currentAlpha = 1f; // Opacité actuelle

    void Update()
    {
        // Calculer la nouvelle opacité en fonction de la vitesse de clignotement
        float deltaAlpha = blinkSpeed * Time.deltaTime;

        // Augmenter ou diminuer l'opacité en fonction de la direction actuelle
        if (isIncreasing)
        {
            currentAlpha += deltaAlpha;
            if (currentAlpha >= maxAlpha)
            {
                currentAlpha = maxAlpha;
                isIncreasing = false;
            }
        }
        else
        {
            currentAlpha -= deltaAlpha;
            if (currentAlpha <= minAlpha)
            {
                currentAlpha = minAlpha;
                isIncreasing = true;
            }
        }

        // Appliquer la nouvelle opacité à l'Image de l'UI
        Color newColor = image.color;
        newColor.a = currentAlpha;
        image.color = newColor;
    }
}
