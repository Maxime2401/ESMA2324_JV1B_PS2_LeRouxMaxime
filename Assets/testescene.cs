using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    // Singleton instance
    public static SceneController Instance;

    private void Awake()
    {
        // Ensure only one instance of SceneController exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to load a new scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Coroutine to load a scene asynchronously
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load the loading screen
        SceneManager.LoadScene("Loading");

        // Wait a frame to allow the loading screen to display
        yield return null;

        // Start loading the new scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        // Wait until the new scene is fully loaded
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                // Activate the new scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
