using UnityEngine;
using System.Collections;

public class teleTrigger : MonoBehaviour
{
    public GameObject shopUI; // Référence à l'interface utilisateur du shop
    public Animator animator; // Référence à l'Animator
    public string groundTag = "Ground"; // Tag du sol pour identifier les collisions
    public Rigidbody2D rb2D; // Référence au Rigidbody2D
    public BoxCollider2D boxCollider2D; // Référence au BoxCollider2D
    public GameObject objectToActivate; // Référence à l'objet à activer

    private void Start()
    {
        // Assurez-vous que le Rigidbody2D et le BoxCollider2D sont attachés à l'objet
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        if (boxCollider2D == null)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Démarrer la coroutine pour afficher l'interface utilisateur après 1 seconde
            StartCoroutine(ShowShopUIAfterDelay(1f));

            // Activer l'autre GameObject si la référence est définie
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }

    private IEnumerator ShowShopUIAfterDelay(float delay)
    {
        // Attendre le délai spécifié (1 seconde)
        yield return new WaitForSeconds(delay);
        // Activer l'interface utilisateur du shop
        if (shopUI != null)
        {
            shopUI.SetActive(true);
            // Démarrer la coroutine pour désactiver l'interface utilisateur après 4 secondes
            StartCoroutine(DisableShopUIAfterDelay(4f));
        }
    }

    private IEnumerator DisableShopUIAfterDelay(float delay)
    {
        // Attendre le délai spécifié (4 secondes)
        yield return new WaitForSeconds(delay);
        // Désactiver l'interface utilisateur du shop
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Arrêter toutes les coroutines pour éviter d'afficher ou désactiver l'interface utilisateur incorrectement
            StopAllCoroutines();
            // Désactiver l'interface utilisateur du shop immédiatement
            if (shopUI != null)
            {
                shopUI.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si l'objet touche le sol
        if (collision.gameObject.CompareTag(groundTag))
        {
            // Changer l'état d'animation
            if (animator != null)
            {
                animator.SetBool("OnGround", true);
                animator.SetTrigger("Tiger");
            }

            // Rendre l'objet statique
            if (rb2D != null)
            {
                rb2D.bodyType = RigidbodyType2D.Static;
            }

            // Rendre le BoxCollider2D un trigger
            if (boxCollider2D != null)
            {
                boxCollider2D.isTrigger = true;
            }
        }
    }
}
