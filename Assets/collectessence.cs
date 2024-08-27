using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameObject targetObject; // L'objet dont le collider sera activé
    public int piecesNeeded = 5; // Nombre de pièces nécessaires pour activer le collider
    public Inventaire inventaire; // Référence à l'inventaire du joueur
    public GameObject objetAApporter; // L'objet qui sera activé

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventaire != null)
            {
                if (inventaire.coinsCount >= piecesNeeded)
                {
                    TargetActivator targetActivator = targetObject.GetComponent<TargetActivator>();
                    if (targetActivator != null)
                    {
                        targetActivator.ActivateCollider();
                        inventaire.RemoveCoins(piecesNeeded); // Retirer le nombre de pièces nécessaires
                        gameObject.SetActive(false);

                        // Activer l'objet public ici
                        if (objetAApporter != null)
                        {
                            objetAApporter.SetActive(true);
                        }
                        else
                        {
                            Debug.LogError("objetAApporter is not assigned!");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Inventaire component not found on the player!");
            }
        }
    }

    void OnDestroy()
    {
        if (inventaire != null)
        {
            inventaire.RemoveCoins(piecesNeeded); // Retirer le nombre de pièces nécessaires lorsque le GameObject est détruit
        }
    }
}
