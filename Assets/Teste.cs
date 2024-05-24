using UnityEngine;

public class WallCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Collision détectée avec un mur.");
        }
    }
}
