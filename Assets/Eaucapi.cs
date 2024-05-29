using UnityEngine;
using System.Collections;

public class WaterInteraction : MonoBehaviour
{
    public GameObject targetObject; // L'objet avec lequel la collision doit être détectée
    public MonoBehaviour[] scriptsToDisable; // Liste des scripts à désactiver lorsque le joueur entre en collision avec l'objet cible
    public MonoBehaviour[] scriptsToEnable; // Liste des scripts à activer lorsque le joueur entre en collision avec l'objet cible

    private bool isInContactWithTarget = false; // Variable pour suivre l'état de collision

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision détectée avec: " + other.gameObject.name);

        if (other.gameObject == targetObject)
        {
            Debug.Log("Collision avec l'objet cible détectée: " + targetObject.name);

            // Désactiver les scripts spécifiés
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = false;
                Debug.Log("Script désactivé: " + script.GetType().Name);
            }
            
            // Activer les scripts spécifiés
            foreach (MonoBehaviour script in scriptsToEnable)
            {
                script.enabled = true;
                Debug.Log("Script activé: " + script.GetType().Name);
            }

            isInContactWithTarget = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Sortie de collision détectée avec: " + other.gameObject.name);

        if (other.gameObject == targetObject)
        {
            Debug.Log("Sortie de collision avec l'objet cible: " + targetObject.name);
            isInContactWithTarget = false;
            StartCoroutine(ChangeScriptsAfterDelay());
        }
    }

    private IEnumerator ChangeScriptsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Attendre 2 secondes

        // Activer les scripts désactivés lors de la collision avec l'objet cible
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true;
            Debug.Log("Script réactivé: " + script.GetType().Name);
        }
        
        // Désactiver les scripts activés lors de la collision avec l'objet cible
        foreach (MonoBehaviour script in scriptsToEnable)
        {
            script.enabled = false;
            Debug.Log("Script désactivé: " + script.GetType().Name);
        }
    }

    void Update()
    {
        // Vérifier l'état de collision chaque frame
        if (isInContactWithTarget)
        {
            Debug.Log("Le joueur est en contact avec l'objet cible.");
            // Vous pouvez ajouter ici tout autre code que vous souhaitez exécuter chaque frame lorsque le joueur est en contact avec l'objet cible
        }
    }
}
