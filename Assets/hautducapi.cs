using UnityEngine;

public class GirafeController : MonoBehaviour
{
    public Transform cou;          // Transform du cou de la girafe
    public Transform tete;         // Transform de la tête de la girafe
    public Transform capybara;     // Transform du capybara
    public Transform pivotInverse; // Transform du pivot inversé
    public float vitesseRotation = 5f; // Vitesse de rotation du cou
    public float distanceSeuil = 0.5f; // Fourchette de distance seuil entre la tête de la girafe et le joueur pour désactiver le suivi

    private Quaternion rotationInitialeCou; // Rotation initiale du cou

    private bool isFollowing = false; // Variable pour indiquer si la girafe suit le capybara

    void Start()
    {
        rotationInitialeCou = cou.rotation;
    }

    void Update()
    {
        if (isFollowing && !EstProcheDeLaTete(capybara.position))
        {    
            // Calculer la direction du capybara par rapport au cou de la girafe
            Vector3 direction = capybara.position - cou.position;

            // Calculer l'angle entre la direction et l'axe horizontal
            float angleCible = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Limiter l'angle cible à un maximum de 50 degrés
            angleCible = Mathf.Clamp(angleCible, -50f, 50f);

            // Appliquer la rotation en douceur au cou de la girafe
            Quaternion rotationCible = Quaternion.Euler(0, 0, angleCible);
            cou.rotation = Quaternion.Slerp(cou.rotation, rotationCible, Time.deltaTime * vitesseRotation);

            // Calculer la rotation inverse pour le pivot
            Quaternion rotationInverse = Quaternion.Inverse(cou.localRotation);

            // Appliquer la rotation inverse au pivot
            pivotInverse.localRotation = rotationInverse;
        }
        else
        {
            isFollowing = false;
        }
    }

    bool EstProcheDeLaTete(Vector3 position)
    {
        // Vérifier si la position donnée est à une distance seuil de la tête de la girafe
        return Mathf.Abs(position.y - tete.position.y) < distanceSeuil;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle encountered, starting following");
            isFollowing = true; // Active le suivi du joueur lorsque la monture entre en collision avec un obstacle
        }
    }
}
