using UnityEngine;

public class ColliderFreeze : MonoBehaviour
{
    public Collider2D colliderToFreeze; // Collider dont le Rigidbody doit devenir cinématique et trigger

    void FixedUpdate()
    {
        // Vérifie si le script est activé et que le collider est défini
        if (enabled && colliderToFreeze != null)
        {
            // Obtenir le Rigidbody2D du collider
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                // Rend le Rigidbody cinématique et bloque l'axe Y
                colliderRb.isKinematic = true;
                colliderRb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
            // Active le mode trigger pour le collider
            colliderToFreeze.isTrigger = true;
        }
    }

    void OnDisable()
    {
        // Lorsque le script est désactivé, réactive la physique du Rigidbody du collider et désactive le mode trigger
        if (colliderToFreeze != null)
        {
            Rigidbody2D colliderRb = colliderToFreeze.GetComponent<Rigidbody2D>();
            if (colliderRb != null)
            {
                // Réactive la physique et enlève les contraintes
                colliderRb.isKinematic = false;
                colliderRb.constraints = RigidbodyConstraints2D.None;
            }
            // Désactive le mode trigger du collider
            colliderToFreeze.isTrigger = false;
        }
    }
}
