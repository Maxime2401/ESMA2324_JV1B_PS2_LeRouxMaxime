using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Déclare une instance statique du script pour qu'une seule instance soit conservée entre les scènes
    private static CharacterMovement instance;

    // Déclaration des variables pour les points de destination et les paramètres de mouvement
    public GameObject[] targetPoints; // Tableau des points de destination en tant que GameObject
    public float speed = 5f; // Vitesse de déplacement du personnage
    public float stoppingDistance = 0.1f; // Distance seuil pour considérer que le personnage a atteint un point
    public bool n0 = true; // Condition pour permettre l'accès au point 1
    public bool n1 = true; // Condition pour permettre l'accès au point 2
    public bool n2 = true; // Condition pour permettre l'accès au point 3
    public bool n3 = true; // Condition pour permettre l'accès au point 4

    private int currentTargetIndex = 0; // Index du point de destination actuel
    private bool isMoving = true; // Indique si le personnage est en mouvement ou non

    public GameObject specialObject1; // GameObject à activer quand n1 est vrai
    public GameObject specialObject2; // GameObject à activer quand n2 est vrai
    public GameObject specialObject3; // GameObject à activer quand n3 est vrai

    void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance de ce script dans le jeu
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Marquez les targetPoints pour ne pas les détruire entre les scènes
            foreach (GameObject targetPoint in targetPoints)
            {
                DontDestroyOnLoad(targetPoint);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Si le personnage est en mouvement, déplacer vers le point de destination actuel
        if (isMoving)
        {
            MoveToTarget();
        }

        // Si le personnage est arrivé au point de destination
        if (!isMoving)
        {
            // Si la touche de déplacement vers la droite est enfoncée et que le personnage peut aller au point suivant
            if (Input.GetKeyDown(KeyCode.RightArrow) && currentTargetIndex < targetPoints.Length - 1 && CanMoveToNextPoint(currentTargetIndex + 1))
            {
                currentTargetIndex++;
                isMoving = true; // Démarre le mouvement vers le nouveau point de destination
            }
            // Si la touche de déplacement vers la gauche est enfoncée et que le personnage peut aller au point précédent
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentTargetIndex > 0 && CanMoveToNextPoint(currentTargetIndex - 1))
            {
                currentTargetIndex--;
                isMoving = true; // Démarre le mouvement vers le nouveau point de destination
            }
        }

        // Activer ou désactiver les objets spéciaux en fonction des booléens
        if (specialObject1 != null)
        {
            specialObject1.SetActive(n1);
        }
        if (specialObject2 != null)
        {
            specialObject2.SetActive(n2);
        }
        if (specialObject3 != null)
        {
            specialObject3.SetActive(n3);
        }
    }

    bool CanMoveToNextPoint(int targetIndex)
    {
        // Vérifie la condition pour permettre l'accès au point cible
        switch (targetIndex)
        {
            case 0: return n0;
            case 1: return n1;
            case 2: return n2;
            case 3: return n3;
            default: return false;
        }
    }

    void MoveToTarget()
    {
        // Vérifier s'il y a des points de destination
        if (targetPoints.Length == 0)
        {
            Debug.LogError("No target points found.");
            return;
        }

        // Calculer la direction vers le point de destination
        Vector3 direction = targetPoints[currentTargetIndex].transform.position - transform.position;

        // Si le personnage n'a pas atteint le point de destination
        if (direction.magnitude > stoppingDistance)
        {
            // Déplacer le personnage vers le point de destination en utilisant la vitesse et le temps écoulé depuis la dernière frame
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Arrêter le personnage car il est assez proche du point de destination
            isMoving = false;
        }
    }
}
