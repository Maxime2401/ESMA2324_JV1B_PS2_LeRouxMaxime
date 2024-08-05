using UnityEngine;
using System.Collections;

public class DesactivationCollider : MonoBehaviour
{
    public BoxCollider2D colliderCible; // Le BoxCollider2D qui déclenchera la désactivation
    public BoxCollider2D colliderCapteur; // Le BoxCollider2D qui fait office de capteur
    public BoxCollider2D colliderADesactiver; // Le BoxCollider2D à désactiver
    public float dureeDesactivation = 2.0f; // Durée pendant laquelle le collider sera désactivé

    private void OnTriggerEnter2D(Collider2D autre)
    {
        // Vérifie si l'objet en collision est le collider spécifié
        if (autre == colliderCible)
        {
            // Démarre la coroutine pour désactiver le collider
            StartCoroutine(DesactiverCollider());
        }
    }

    private IEnumerator DesactiverCollider()
    {
        // Désactive le collider spécifié
        colliderADesactiver.enabled = false;
        
        // Attend la durée spécifiée
        yield return new WaitForSeconds(dureeDesactivation);
        
        // Réactive le collider spécifié
        colliderADesactiver.enabled = true;
    }
}
