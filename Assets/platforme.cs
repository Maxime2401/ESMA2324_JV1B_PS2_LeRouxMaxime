using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    public Collider2D triggerCollider; // Le collider qui détecte le joueur pour désactiver la plateforme
    public Collider2D platformCollider; // Le collider de la plateforme qui sera désactivé

    private void Start()
    {
        // Assurez-vous que le triggerCollider est configuré comme un trigger
        if (!triggerCollider.isTrigger)
        {
            Debug.LogWarning("Le triggerCollider doit être configuré comme un trigger.");
        }

        // Assurez-vous que le platformCollider n'est pas un trigger
        if (platformCollider.isTrigger)
        {
            Debug.LogWarning("Le platformCollider ne doit pas être configuré comme un trigger.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Désactiver temporairement le collider de la plateforme
            platformCollider.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Réactiver le collider de la plateforme lorsque le joueur quitte la collision
            platformCollider.enabled = true;
        }
    }
}
