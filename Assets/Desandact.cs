using UnityEngine;

public class ActivateScriptsWithPlayerChild : MonoBehaviour
{
    // Référence au GameObject représentant le joueur
    public GameObject playerObject;

    // Références aux scripts à activer ou désactiver
    public MonoBehaviour[] scriptsToActivate; // Scripts à activer
    public MonoBehaviour[] scriptsToDeactivate; // Scripts à désactiver

    void Update()
    {
        // Vérifie si le joueur est un enfant de cet objet
        bool playerIsChild = playerObject != null && playerObject.transform.IsChildOf(transform);

        // Active ou désactive les scripts en fonction de la présence du joueur comme enfant
        foreach (MonoBehaviour script in scriptsToActivate)
        {
            if (script != null)
            {
                script.enabled = playerIsChild;
            }
        }

        foreach (MonoBehaviour script in scriptsToDeactivate)
        {
            if (script != null)
            {
                script.enabled = !playerIsChild;
            }
        }
    }
}
