using UnityEngine;

public class WaterInteraction : MonoBehaviour
{
    public MonoBehaviour[] scriptsToDisable; // Liste des scripts à désactiver lorsque le joueur entre en collision avec l'eau
    public MonoBehaviour[] scriptsToEnable; // Liste des scripts à activer lorsque le joueur entre en collision avec l'eau

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Désactiver les scripts spécifiés
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = false;
            }
            
            // Activer les scripts spécifiés
            foreach (MonoBehaviour script in scriptsToEnable)
            {
                script.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Activer les scripts désactivés lors de la collision avec l'eau
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = true;
            }
            
            // Désactiver les scripts activés lors de la collision avec l'eau
            foreach (MonoBehaviour script in scriptsToEnable)
            {
                script.enabled = false;
            }
        }
    }
}
