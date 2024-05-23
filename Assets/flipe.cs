using UnityEngine;

public class ScaleByDirection : MonoBehaviour
{
    private float previousX;
    private Transform objectTransform;

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

        // Si la position actuelle en X est supérieure à la précédente, mettre l'échelle sur l'axe X à 5
        if (currentX > previousX)
        {
            objectTransform.localScale = new Vector3(5f, objectTransform.localScale.y, objectTransform.localScale.z);
        }
        // Sinon, si la position actuelle en X est inférieure à la précédente, mettre l'échelle sur l'axe X à -5
        else if (currentX < previousX)
        {
            objectTransform.localScale = new Vector3(-5f, objectTransform.localScale.y, objectTransform.localScale.z);
        }

        // Mettre à jour la position précédente avec la position actuelle en X
        previousX = currentX;
    }
}
