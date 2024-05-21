using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Nom de la scène à charger

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si le GameObject en contact a le tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur est en collision avec ce GameObject.");
        }
    }

    void Update()
    {
        // Vérifier si la touche "S" est enfoncée
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("La touche S est enfoncée.");

            // Charger la scène spécifiée
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
