using UnityEngine;

public class TargetActivator : MonoBehaviour
{
    private Collider2D targetCollider;

    void Start()
    {
        targetCollider = GetComponent<Collider2D>();
        if (targetCollider != null)
        {
            targetCollider.enabled = false; // Désactiver le collider au début
        }
    }

    public void ActivateCollider()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = true; // Activer le collider
        }
    }
}
