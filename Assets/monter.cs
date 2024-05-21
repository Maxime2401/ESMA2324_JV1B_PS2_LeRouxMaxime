using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MountInteraction : MonoBehaviour
{
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
        // Vérifie si la touche d'interaction est enfoncée
        if (Input.GetKeyDown(interactKey))
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
        Cri.enabled=false;
        suivicou.enabled=false;
        currentMount = FindClosestMount();

        if (currentMount != null)
        {
            // Calcul de la distance entre le joueur et la monture
            float distanceToMount = Vector3.Distance(transform.position, currentMount.position);

            // Si la distance est inférieure à la distance d'attachement spécifiée, attache le joueur à la monture
            if (distanceToMount <= mountAttachDistance)
            {
                // Déplacer progressivement le joueur vers la monture
                yield return StartCoroutine(MovePlayerToMount());

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
                Debug.Log("Monture trop éloignée pour être attachée.");
                // Réactiver le script de mouvement du joueur si l'attachement échoue
                playerControllerScript.enabled = true;
                suivicou.enabled=true;
                Cri.enabled=true;
            }
        }
        else
        {
            Debug.Log("Aucune monture trouvée à distance d'attachement.");
            // Réactiver le script de mouvement du joueur si aucune monture n'est trouvée
            playerControllerScript.enabled = true;
            suivicou.enabled=true;
            Cri.enabled=true;
        }
    }

    IEnumerator MovePlayerToMount()
    {
        float moveSpeed = 5f; // Vitesse de déplacement du joueur vers la monture

        while (Vector3.Distance(transform.position, currentMount.position + mountAttachOffset) > 0.1f)
        {
            // Calculer la direction vers la monture
            Vector3 directionToMount = (currentMount.position + mountAttachOffset - transform.position).normalized;

            // Calculer la nouvelle position du joueur
            Vector3 newPosition = transform.position + directionToMount * moveSpeed * Time.deltaTime;

            // Déplacer le joueur vers la nouvelle position
            transform.position = newPosition;

            // Attendre le prochain frame
            yield return null;
        }

        // S'assurer que le joueur atteint la position exacte
        transform.position = currentMount.position + mountAttachOffset;
    }

    void DetachFromMount()
    {
        // Détache le joueur de la monture
        transform.parent = null; // Réinitialise le parent du joueur
        isAttached = false;

        // Réactiver le script de mouvement du joueur
        playerControllerScript.enabled = true;
        suivicou.enabled=true;
        Cri.enabled=true;

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
}
