using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MountInteraction : MonoBehaviour
{
    public bool peuxsata = false;
    public KeyCode interactKey = KeyCode.H; // Touche pour interagir avec la monture
    public List<GameObject> mounts = new List<GameObject>(); // Liste des montures
    public float mountAttachDistance = 1.5f; // Distance à laquelle le joueur sera attaché à la monture
    public Vector3 mountAttachOffset = new Vector3(0f, 0.5f, 0f); // Offset pour ajuster la position du joueur sur la monture
    public MonoBehaviour playerControllerScript; // Script de mouvement du joueur
    public MonoBehaviour Cri;
    public MonoBehaviour suivicou;
    public MonoBehaviour mountControllerScript; // Script de contrôle de la monture
    public MonoBehaviour[] additionalScripts; // Scripts supplémentaires à activer/désactiver

    private bool isAttached = false; // Indique si le joueur est attaché à la monture
    private Transform currentMount; // Référence à la monture actuelle

    void Start()
    {
        if (playerControllerScript == null || mountControllerScript == null)
        {
            Debug.LogError("PlayerControllerScript ou MountControllerScript n'a pas été défini.");
        }
    }

    void Update()
    {
        // Vérifie si la touche d'interaction est enfoncée et que le joueur est en collision avec peuxsata
        if (Input.GetKeyDown(interactKey) && peuxsata)
        {
            if (isAttached)
            {
                DetachFromMount(); // Si le joueur est déjà attaché, détache-le
            }
            else
            {
                StartCoroutine(AttachToMount()); // Sinon, attache-le à la monture
            }
        }
    }

    IEnumerator AttachToMount()
    {
        // Désactiver immédiatement le script de mouvement du joueur
        playerControllerScript.enabled = false;
        Cri.enabled = false;
        suivicou.enabled = false;
        currentMount = FindClosestMount();

        if (currentMount != null)
        {
            // Calcul de la distance entre le joueur et la monture
            float distanceToMount = Vector3.Distance(transform.position, currentMount.position);

            // Si la distance est inférieure à la distance d'attachement spécifiée, attache le joueur à la monture
            if (distanceToMount <= mountAttachDistance)
            {
                // Lancer la Coroutine pour déplacer le joueur vers la monture avec une limite de temps
                yield return StartCoroutine(MovePlayerToMountWithTimeout(2f));

                if (isAttached) // Vérifie si l'attachement a réussi avant de continuer
                {
                    // Attache le joueur à la monture
                    transform.parent = currentMount; // La monture devient le parent du joueur
                    isAttached = true;

                    // S'assurer que le joueur est parfaitement aligné avec la monture
                    transform.localPosition = mountAttachOffset;

                    // Activer le script de contrôle de la monture
                    mountControllerScript.enabled = true;

                    // Activer les scripts supplémentaires
                    foreach (MonoBehaviour script in additionalScripts)
                    {
                        script.enabled = true;
                    }
                }
                else
                {
                    // Réactiver le script de mouvement du joueur si l'attachement échoue
                    playerControllerScript.enabled = true;
                    suivicou.enabled = true;
                    Cri.enabled = true;
                }
            }
            else
            {
                Debug.Log("Monture trop éloignée pour être attachée.");
                // Réactiver le script de mouvement du joueur si l'attachement échoue
                playerControllerScript.enabled = true;
                suivicou.enabled = true;
                Cri.enabled = true;
            }
        }
        else
        {
            Debug.Log("Aucune monture trouvée à distance d'attachement.");
            // Réactiver le script de mouvement du joueur si aucune monture n'est trouvée
            playerControllerScript.enabled = true;
            suivicou.enabled = true;
            Cri.enabled = true;
        }
    }

    IEnumerator MovePlayerToMountWithTimeout(float timeout)
    {
        float moveSpeed = 5f; // Vitesse de déplacement du joueur vers la monture
        float elapsedTime = 0f;

        while (Vector3.Distance(transform.position, currentMount.position + mountAttachOffset) > 0.1f)
        {
            // Calculer la direction vers la monture
            Vector3 directionToMount = (currentMount.position + mountAttachOffset - transform.position).normalized;

            // Calculer la nouvelle position du joueur
            Vector3 newPosition = transform.position + directionToMount * moveSpeed * Time.deltaTime;

            // Déplacer le joueur vers la nouvelle position
            transform.position = newPosition;

            // Augmenter le temps écoulé
            elapsedTime += Time.deltaTime;

            // Vérifier si le temps écoulé dépasse le timeout
            if (elapsedTime > timeout)
            {
                DetachFromMount(); // Détacher le joueur si le timeout est dépassé
                yield break;
            }

            // Attendre le prochain frame
            yield return null;
        }

        // S'assurer que le joueur atteint la position exacte
        transform.position = currentMount.position + mountAttachOffset;
        isAttached = true; // Indiquer que le joueur est attaché après avoir atteint la position
    }

    void DetachFromMount()
    {
        // Détache le joueur de la monture
        transform.parent = null; // Réinitialise le parent du joueur
        isAttached = false;

        // Réactiver le script de mouvement du joueur
        playerControllerScript.enabled = true;
        suivicou.enabled = true;
        Cri.enabled = true;

        // Désactiver le script de contrôle de la monture
        mountControllerScript.enabled = false;

        // Désactiver les scripts supplémentaires
        foreach (MonoBehaviour script in additionalScripts)
        {
            script.enabled = false;
        }
    }

    Transform FindClosestMount()
    {
        Transform closestMount = null;
        float closestDistance = mountAttachDistance;

        foreach (GameObject mount in mounts)
        {
            float distance = Vector3.Distance(transform.position, mount.transform.position);
            if (distance < closestDistance)
            {
                closestMount = mount.transform;
                closestDistance = distance;
            }
        }

        return closestMount;
    }

    // Définir la variable peuxsata sur true lorsqu'on entre en collision avec l'objet ayant le tag "peuxmonter"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("peuxmonter"))
        {
            peuxsata = true;
        }
    }

    // Si on n'est plus en collision avec un objet ayant le tag "monter object" et qu'on est attaché à une monture, détacher le joueur
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("peuxmonter") && isAttached)
        {
            DetachFromMount();
        }
        if (other.CompareTag("peuxmonter"))
        {
            peuxsata = false;
        }
    }
}
