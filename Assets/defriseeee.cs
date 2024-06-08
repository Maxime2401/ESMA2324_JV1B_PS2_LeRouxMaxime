using UnityEngine;

public class ColliderFreeze2 : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Aucun Rigidbody2D trouvé sur ce GameObject.");
        }
    }

    void OnEnable()
    {
        // Lorsque le script est activé, dégele les positions X et Y
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnDisable()
    {
        // Lorsque le script est désactivé, géle les positions X et Y
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
