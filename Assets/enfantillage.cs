using UnityEngine;

public class PlayerChildOnContact : MonoBehaviour
{
    public GameObject targetObject; // L'objet cible assigné via l'inspecteur

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == targetObject)
        {
            transform.parent = null;
        }
    }
}
