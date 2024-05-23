using UnityEngine;

public class MultiToggleActiveState : MonoBehaviour
{
    // Variables publiques pour les conditions
    public bool n0;
    public bool n1;
    public bool n3;
    public bool n4;

    // Références aux GameObjects à activer/désactiver
    public GameObject targetGameObject0;
    public GameObject targetGameObject1;
    public GameObject targetGameObject3;
    public GameObject targetGameObject4;

    // Référence au script CharacterMovement
    private CharacterMovement characterMovement;

    void Start()
    {
        // Rechercher et assigner le script CharacterMovement
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            characterMovement = player.GetComponent<CharacterMovement>();
            if (characterMovement == null)
            {
                Debug.LogError("CharacterMovement script not found on the Player!");
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        // Active ou désactive les GameObjects en fonction de la valeur des booléens
        if (targetGameObject0 != null) targetGameObject0.SetActive(n0);
        if (targetGameObject1 != null) targetGameObject1.SetActive(n1);
        if (targetGameObject3 != null) targetGameObject3.SetActive(n3);
        if (targetGameObject4 != null) targetGameObject4.SetActive(n4);

        // Si n2 dans CharacterMovement est vrai, exécuter la logique correspondante
        if (characterMovement != null && characterMovement.n2)
        {
            // Ajoutez ici la logique spécifique à exécuter lorsque n2 est vrai
            Debug.Log("n2 est vrai dans CharacterMovement");
        }
    }
}

