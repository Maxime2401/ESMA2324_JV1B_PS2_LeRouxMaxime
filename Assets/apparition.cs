using UnityEngine;
using UnityEngine.UI;

public class ActivateObjectOnButtonPress : MonoBehaviour
{
    public GameObject targetObject; // L'objet à activer/désactiver
    public Button activateButton; // Le bouton qui déclenche l'activation/désactivation

    void Start()
    {
        // Assurez-vous que le bouton et l'objet cible sont assignés
        if (activateButton != null && targetObject != null)
        {
            // Ajouter un listener pour le bouton
            activateButton.onClick.AddListener(ToggleTargetObject);
        }
        else
        {
            Debug.LogWarning("Le bouton ou l'objet cible n'est pas assigné dans l'inspecteur.");
        }
    }

    void ToggleTargetObject()
    {
        if (targetObject != null)
        {
            // Bascule l'état actif de l'objet
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);
            Debug.Log("Objet " + (isActive ? "désactivé" : "activé") + ": " + targetObject.name);
        }
    }
}
