using UnityEngine;

public class ColliderFreeze : MonoBehaviour
{
    public Collider2D colliderToFreeze; // Collider dont le Rigidbody doit devenir statique

    void FixedUpdate()
    {
        // Vérifie si le script est activé et que le collider est défini
        if (enabled && colliderToFreeze != null)
        {
            // Rend le Rigidbody du collider statique
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                colliderRb.isKinematic = true;
            }
        }
    }

    void OnDisable()
    {
        // Lorsque le script est désactivé, réactive la physique du Rigidbody du collider si nécessaire
        if (colliderToFreeze != null)
        {
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                colliderRb.isKinematic = false;
            }
        }
    }
}
