using UnityEngine;

public class ActivateBoolOnCollision : MonoBehaviour
{
    public GameObject player; // Référence publique à l'objet du joueur
    private CharacterMovement characterMovement; // Référence au script CharacterMovement

    void Start()
    {
        // Si l'objet joueur n'est pas assigné dans l'inspecteur, rechercher par le tag "Player"
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player object not found!");
                return;
            }
        }

        // Rechercher et assigner le script CharacterMovement
        characterMovement = player.GetComponent<CharacterMovement>();
        if (characterMovement == null)
        {
            Debug.LogError("CharacterMovement script not found on the Player!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activer le booléen n1 dans CharacterMovement
            if (characterMovement != null)
            {
                characterMovement.n1 = true;
            }
            else
            {
                
            }
        }
    }
}
