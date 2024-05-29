using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;     // Tableau contenant tous les arrière-plans à parcourir
    public float[] parallaxScales;      // Proportion du mouvement de la caméra à appliquer aux arrière-plans
    public float smoothing = 1f;        // Lissage du mouvement parallaxe
    public bool inverseParallax = false; // Inverser le mouvement parallaxe

    private Transform cam;              // Référence à la transform de la caméra
    private Vector3 previousCamPosition;    // Position de la caméra dans le frame précédent

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPosition = cam.position;

        if (backgrounds.Length != parallaxScales.Length)
        {
            Debug.LogError("Le nombre d'arrière-plans et de parallaxScales doit être le même.");
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Calculer le mouvement parallaxe en fonction du changement de position de la caméra
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            // Inverser le mouvement parallaxe si nécessaire
            if (inverseParallax)
            {
                parallax = -parallax;
            }

            // Calculer la position cible pour l'arrière-plan actuel
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Créer une position cible à laquelle se déplacer avec le lissage
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Déplacer l'arrière-plan vers la nouvelle position cible
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Mettre à jour la position précédente de la caméra
        previousCamPosition = cam.position;
    }
}
