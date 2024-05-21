using UnityEngine;
using System.Collections;

public class MounturDetection : MonoBehaviour
{
    public GameObject mountIcon; // Référence à l'icône de monture dans l'inspecteur

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mountIcon.SetActive(true); // Afficher l'icône de monture lorsque le personnage entre en collision avec la monture
            StartCoroutine(DisableMountIconAfterDelay(3f)); // Désactiver l'icône après 3 secondes
        }
    }

    IEnumerator DisableMountIconAfterDelay(float delay)
    {
        // Attendre pendant le délagdgi spécifié
        yield return new WaitForSeconds(delay);

        // Désactiver l'icône de monture
        mountIcon.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mountIcon.SetActive(false); // Masquer l'icône de monture lorsque le personnage sort de la collision avec la monture
        }
    }
}
