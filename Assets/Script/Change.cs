using UnityEngine;
using UnityEngine.SceneManagement;

public class Change: MonoBehaviour
{
    public string sceneName; // Nom de la scène à charger (doit correspondre exactement au nom de la scène dans votre projet)

    public void LoadSceneOnClick()
    {
        Debug.Log("Chargement de la scène : " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
