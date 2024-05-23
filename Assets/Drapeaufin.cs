using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangerFinniveau : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la scène à charger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeSceneAfterDelay(3f)); // Délai de 3 secondes
        }
    }

    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
