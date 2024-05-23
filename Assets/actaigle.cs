using UnityEngine;

public class ActivateObjectsOnCollision : MonoBehaviour
{
    // Références aux GameObjects à activer
    public GameObject[] objectsToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            // Active chaque objet spécifié
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
