using UnityEngine;

public class ShowSpriteOnCollision : MonoBehaviour
{
    // Référence au SpriteRenderer à activer/désactiver
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        // Assurez-vous que le SpriteRenderer est désactivé au début
        spriteRenderer.enabled = false;
    }

    // Méthode appelée lorsque le joueur entre en collision avec le collider de ce GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (other.CompareTag("Player"))
        {
            // Active le SpriteRenderer pour rendre le sprite visible
            spriteRenderer.enabled = true;
        }
    }

    // Méthode appelée lorsque le joueur quitte la collision avec le collider de ce GameObject
    void OnTriggerExit2D(Collider2D other)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (other.CompareTag("Player"))
        {
            // Désactive le SpriteRenderer pour rendre le sprite invisible
            spriteRenderer.enabled = false;
        }
    }

    void Update()
    {
        // Vérifie si la touche H est enfoncée et si le joueur est sur l'objet
        if (Input.GetKeyDown(KeyCode.H) && spriteRenderer.enabled)
        {
            // Désactive le SpriteRenderer pour rendre le sprite invisible
            spriteRenderer.enabled = false;
        }
    }
}
