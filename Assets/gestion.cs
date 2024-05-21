using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    // Le script de condition à vérifier
    public MonoBehaviour scriptManager;

    // Le GameObject à vérifier
    public GameObject targetGameObject;

    // Le script à désactiver
    public MonoBehaviour scriptToDisable;

    // Le premier script à activer
    public MonoBehaviour scriptToEnable1;

    // Le deuxième script à activer
    public MonoBehaviour scriptToEnable2;

    // Liste de scripts à activer lorsque ce script est activé
    public MonoBehaviour[] scriptsToEnableOnActivate;

    // Liste de scripts à désactiver lorsque ce script est activé
    public MonoBehaviour[] scriptsToDisableOnActivate;

    void Start()
    {
        // Vérifie si le scriptManager est activé
        if (scriptManager != null && scriptManager.enabled)
        {
            Debug.Log(targetGameObject.name + " is a child of " + gameObject.name);

            // Désactiver le script
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = false;
                Debug.Log(scriptToDisable.GetType().Name + " has been disabled.");
            }

            // Activer les scripts
            if (scriptToEnable1 != null)
            {
                scriptToEnable1.enabled = true;
                Debug.Log(scriptToEnable1.GetType().Name + " has been enabled.");
            }
            if (scriptToEnable2 != null)
            {
                scriptToEnable2.enabled = true;
                Debug.Log(scriptToEnable2.GetType().Name + " has been enabled.");
            }

            // Activer les scripts spécifiés lors de l'activation
            foreach (var script in scriptsToEnableOnActivate)
            {
                script.enabled = true;
                Debug.Log(script.GetType().Name + " has been enabled.");
            }

            // Désactiver les scripts spécifiés lors de l'activation
            foreach (var script in scriptsToDisableOnActivate)
            {
                script.enabled = false;
                Debug.Log(script.GetType().Name + " has been disabled.");
            }
        }
    }
}
