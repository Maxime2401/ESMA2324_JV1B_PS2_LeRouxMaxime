using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventaire : MonoBehaviour
{
    public int coinsCount;
    public Text countText;

    public static Inventaire instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Pour que ce GameObject persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruire les doublons
            return;
        }

        if (countText == null)
        {
            Debug.LogError("Text component is not found in the scene!");
        }

        // Écouter l'événement de chargement de scène pour mettre à jour le Text lorsqu'une nouvelle scène est chargée
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Retirer l'écouteur de l'événement de chargement de scène lors de la destruction de l'objet
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (countText == null)
        {
            Debug.LogError("Text component is not found in the scene!");
        }

        // Mettre à jour le Text avec le nombre actuel de pièces
        UpdateCoinsText();
    }

    public void AddCoins(int count) // permet d'ajouter les pièces en UI
    {
        coinsCount += count;
        UpdateCoinsText();
    }

    public void RemoveCoins(int count) // Permet de retirer les pièces en UI
    {
        coinsCount -= count;
        if (coinsCount < 0)
        {
            coinsCount = 0; // Assurer que le nombre de pièces ne devient pas négatif
        }
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        // Mettre à jour le Text avec le nombre actuel de pièces
        if (countText != null)
        {
            countText.text = coinsCount.ToString();
        }
        else
        {
            Debug.LogError("Text component is not assigned!");
        }
    }
}
