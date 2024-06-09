using UnityEngine;

public class oiseauuuController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du joueur
    public GameObject targetObject; // Référence à l'objet cible
    private bool shouldMoveLeft = false; // Indicateur pour savoir si le joueur doit se déplacer vers la gauche

    void Update()
    {
        // Si l'indicateur est activé, déplacer le joueur vers la gauche
        if (shouldMoveLeft)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    // Détecter les collisions avec d'autres objets
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur entre en collision avec l'objet cible assigné
        if (collision.gameObject == targetObject)
        {
            shouldMoveLeft = true; // Activer l'indicateur de déplacement vers la gauche
        }
    }

    // Si vous voulez que le joueur arrête de se déplacer quand il quitte l'objet
    void OnCollisionExit2D(Collision2D collision)
    {
        // Si le joueur quitte la collision avec l'objet cible assigné
        if (collision.gameObject == targetObject)
        {
            shouldMoveLeft = false; // Désactiver l'indicateur de déplacement vers la gauche
        }
    }
}
