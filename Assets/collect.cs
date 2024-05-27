using UnityEngine;

public class Collectible1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Vérifiez si l'instance de Inventaire est nulle et essayez de la trouver
            if (Inventaire.instance == null)
            {
                Inventaire.instance = FindObjectOfType<Inventaire>();

                // Si Inventaire.instance est toujours null, affichez un message d'erreur
                if (Inventaire.instance == null)
                {
                    Debug.LogError("Aucune instance de Inventaire trouvée dans la scène.");
                    return;
                }
            }

            Inventaire.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
