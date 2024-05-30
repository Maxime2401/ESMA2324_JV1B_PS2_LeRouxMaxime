using UnityEngine;

public class ScaleByDirection : MonoBehaviour
{
    private float previousX;
    private Transform objectTransform;
    public float threshold = 0.1f; // Ajustez cette valeur selon vos besoins
    public float scaleValue = 5f; // Valeur de l'échelle sur l'axe X

    void Start()
    {
        // Récupérer le composant Transform de l'objet
        objectTransform = transform;
        // Initialiser la position précédente avec la position actuelle en X
        previousX = objectTransform.position.x;
    }

    void Update()
    {
        // Récupérer la position actuelle en X
        float currentX = objectTransform.position.x;

        // Vérifier si le changement de position en X dépasse le seuil
        if (Mathf.Abs(currentX - previousX) >= threshold)
        {
            // Appliquer l'échelle sur l'axe X en fonction de la direction
            objectTransform.localScale = new Vector3((currentX > previousX) ? scaleValue : -scaleValue, objectTransform.localScale.y, objectTransform.localScale.z);

            // Mettre à jour la position précédente avec la position actuelle en X
            previousX = currentX;
        }
    }
}
