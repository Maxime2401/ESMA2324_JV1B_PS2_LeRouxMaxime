using UnityEngine;

public class DisableObjectAtStart : MonoBehaviour
{
    public GameObject objectToDisable; // L'objet à désactiver

    void Start()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Désactiver l'objet au démarrage de la scène
        }
        else
        {
            Debug.LogError("L'objet à désactiver n'est pas assigné dans le script DisableObjectAtStart sur l'objet : " + gameObject.name);
        }
    }
}
