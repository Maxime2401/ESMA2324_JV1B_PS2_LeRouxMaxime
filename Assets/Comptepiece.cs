using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public int coinsCount;
    public Text CountText;

    public static Inventaire instance;

    private void Awake()
    { 
        CountText = GameObject.Find("Textpiece").GetComponent<Text>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Pour que ce GameObject persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruire les doublons
        }
    }

    public void AddCoins(int count) // permet d'ajouter les goutes en UI
    {
        coinsCount += count;
        CountText.text = coinsCount.ToString();
    }
}
