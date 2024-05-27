using UnityEngine;

public class MainObjectCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Le joueur est entré en collision avec cet objet
            ObjectFollow[] objectsToFollow = FindObjectsOfType<ObjectFollow>();
            foreach (ObjectFollow obj in objectsToFollow)
            {
                obj.StartFollowing(collision.transform);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Le joueur a quitté la collision avec cet objet
            ObjectFollow[] objectsToFollow = FindObjectsOfType<ObjectFollow>();
            foreach (ObjectFollow obj in objectsToFollow)
            {
                obj.StopFollowing();
            }
        }
    }
}