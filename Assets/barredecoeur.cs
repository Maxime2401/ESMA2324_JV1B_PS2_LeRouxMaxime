using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartDisplay : MonoBehaviour
{
    public Image heartPrefab; // Préfabriqué de l'image du cœur
    public int maxHearts = 3; // Nombre maximum de cœurs à afficher
    public Vector2 heartSpacing = new Vector2(30f, 0f); // Espacement entre les cœurs

    private List<Image> hearts = new List<Image>(); // Liste des images de cœur

    void Start()
    {
        CreateHearts();
    }

    void CreateHearts()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            Image heart = Instantiate(heartPrefab, transform);
            heart.rectTransform.anchoredPosition = new Vector2(i * heartSpacing.x, 0f);
            hearts.Add(heart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; // Activer le cœur s'il reste de la santé
            }
            else
            {
                hearts[i].enabled = false; // Désactiver le cœur s'il n'y a plus de santé
            }
        }
    }
}
