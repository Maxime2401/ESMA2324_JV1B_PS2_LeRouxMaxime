using UnityEngine;

public class MountMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse de déplacement de la monture
    public float moveDistance = 5f; // Distance à parcourir dans chaque direction

    public GameObject animatorObject; // GameObject contenant l'Animator
    private Animator animator; // Référence à l'Animator
    private float initialPositionX;
    private bool moveRight = true;
    public GameObject objectToSpawn; // GameObject à faire apparaître
    private bool isVPressed = false; // Indique si la touche "V" est enfoncée
    private float spawnTimer = 1f; // Durée pendant laquelle l'objet apparaîtra

    void Start()
    {
        // Récupérer l'Animator à partir de l'objet assigné
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Animator Object is not assigned.");
        }

        initialPositionX = transform.position.x; // Enregistre la position initiale de la monture
        enabled = false; // Désactiver le script au début
    }

    void Update()
    {
        // Contrôles au clavier
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            moveRight = true;
        }
        else if (horizontalInput < 0)
        {
            moveRight = false;
        }

        // Déplacement de la monture
        if (!isVPressed) // Ne déplace que si la touche "V" n'est pas enfoncée
        {
            if (moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // Déplacement vers la droite
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // Déplacement vers la gauche
            }
        }

        // Gestion de l'apparition de l'objet
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (animator != null)
            {
                animator.SetBool("ATT", true);
            }
            isVPressed = true;
            objectToSpawn.SetActive(true); // Activer l'objet à faire apparaître
            Invoke("DisableSpawnedObject", spawnTimer); // Désactiver l'objet après une seconde
        }
    }

    void DisableSpawnedObject()
    {
        if (animator != null)
        {
            animator.SetBool("ATT", false);
        }
        objectToSpawn.SetActive(false); // Désactiver l'objet après le délai spécifié
        isVPressed = false; // Réinitialiser la variable de la touche "V" enfoncée
    }
}
