using UnityEngine;
using UnityEngine.UI;

public class ActivateObjectOnButtonPress : MonoBehaviour
{
    public GameObject targetObject; // L'objet à activer
    public Button activateButton; // Le bouton qui déclenche l'activation

    void Start()
    {
        // Assurez-vous que le bouton et l'objet cible sont assignés
        if (activateButton != null && targetObject != null)
        {
            // Ajouter un listener pour le bouton
            activateButton.onClick.AddListener(ActivateTargetObject);
        }
        else
        {
            Debug.LogWarning("Le bouton ou l'objet cible n'est pas assigné dans l'inspecteur.");
        }
    }

    void ActivateTargetObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log("Objet activé: " + targetObject.name);
        }
    }
}
