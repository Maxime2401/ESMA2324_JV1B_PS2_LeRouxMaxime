using UnityEngine;
using UnityEngine.UI;

public class transormeObjectOnButtonPress : MonoBehaviour
{
    public GameObject targetObject; // L'objet à déplacer
    public Button activateButton; // Le bouton qui déclenche le déplacement
    public Vector3 newPosition; // La nouvelle position de l'objet

    void Start()
    {
        // Assurez-vous que le bouton et l'objet cible sont assignés
        if (activateButton != null && targetObject != null)
        {
            // Ajouter un listener pour le bouton
            activateButton.onClick.AddListener(MoveTargetObject);
        }
        else
        {
            Debug.LogWarning("Le bouton ou l'objet cible n'est pas assigné dans l'inspecteur.");
        }
    }

    void MoveTargetObject()
    {
        if (targetObject != null)
        {
            // Déplacer l'objet à la nouvelle position
            targetObject.transform.position = newPosition;
            Debug.Log("Objet déplacé à la nouvelle position: " + newPosition);
        }
    }
}
