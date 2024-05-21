using UnityEngine;

public class MultiToggleActiveState : MonoBehaviour
{
    // Variables publiques pour les conditions
    public bool n0 ;
    public bool n1;
    public bool n3;
    public bool n4;

    // Références aux GameObjects à activer/désactiver
    public GameObject targetGameObject0;
    public GameObject targetGameObject1;
    public GameObject targetGameObject3;
    public GameObject targetGameObject4;

    // Méthode appelée à chaque frame
    void Update()
    {
        // Active ou désactive les GameObjects en fonction de la valeur des booléens
        if (targetGameObject0 = null) targetGameObject0.SetActive(n0);
        if (targetGameObject1 != null) targetGameObject1.SetActive(n1);
        if (targetGameObject3 != null) targetGameObject3.SetActive(n3);
        if (targetGameObject4 != null) targetGameObject4.SetActive(n4);
    }
}
