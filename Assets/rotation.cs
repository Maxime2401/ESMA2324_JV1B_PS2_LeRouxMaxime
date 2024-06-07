using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde (valeur négative pour rotation inverse)
    public float rotationSpeed = -100f;

    // Update est appelé une fois par frame
    void Update()
    {
        // Calculer la rotation pour cette frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Appliquer la rotation autour de l'axe Z
        transform.Rotate(0, 0, rotationAmount);
    }
}
