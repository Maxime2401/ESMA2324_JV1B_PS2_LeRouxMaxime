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
    private KeyBindingsManager keyBindingsManager;
    private bool isVPressed = false; // Indique si la touche "V" est enfoncée
    private float spawnTimer = 1f; // Durée pendant laquelle l'objet apparaîtra

    void Start()
    {   
        keyBindingsManager = FindObjectOfType<KeyBindingsManager>();
        // Récupérer l'Animator à partir de l'objet assigné
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>();
        }

        initialPositionX = transform.position.x; // Enregistre la position initiale de la monture
        enabled = false; // Désactiver le script au début
    }

    void Update()
    {
        if (keyBindingsManager == null)
        {
            return;
        }

        KeyCode moveLeftKey = keyBindingsManager.GetKeyCodeForAction("MoveLeft");
        KeyCode moveRightKey = keyBindingsManager.GetKeyCodeForAction("MoveRight");
        KeyCode capa1Key = keyBindingsManager.GetKeyCodeForAction("Capa1");

        // Contrôles au clavier
        if (moveLeftKey != KeyCode.None && Input.GetKey(moveLeftKey))
        {
            moveRight = false;
        }
        else if (moveRightKey != KeyCode.None && Input.GetKey(moveRightKey))
        {
            moveRight = true;
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

        // Capacité spéciale
        if (capa1Key != KeyCode.None && Input.GetKeyDown(capa1Key))
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
