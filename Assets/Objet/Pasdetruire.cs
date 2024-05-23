using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private void Awake()
    {
        // Détacher l'objet parent de la hiérarchie pour le conserver entre les scènes
        transform.SetParent(null, true);
        DontDestroyOnLoad(gameObject);
    }
}
