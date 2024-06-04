using UnityEngine;

public class DisappearAndReappear : MonoBehaviour
{
    public float disappearTime = 3f; // Temps en secondes avant que l'objet disparaisse après avoir été touché par le joueur
    public float reappearDelay = 5f; // Délai en secondes avant que l'objet réapparaisse
    public string touchBoolName; // Nom du booléen pour l'animation de toucher
    public string disappearBoolName; // Nom du booléen pour l'animation de disparition

    private bool isDisabled = false; // Indique si l'objet est désactivé
    private Animator animator; // Référence au composant Animator

    void Start()
    {
        isDisabled = false; // S'assurer que l'objet commence activé
        animator = GetComponent<Animator>(); // Obtenir le composant Animator
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool(touchBoolName, true); // Activer le booléen pour l'animation de toucher
            Invoke("PlayDisappearAnimation", disappearTime - 0.35f); // Programmer l'animation de disparition
            Invoke("Disappear", disappearTime); // Programmer la disparition de l'objet après le temps spécifié
        }
    }

    // Fonction pour jouer l'animation de disparition
    void PlayDisappearAnimation()
    {
        animator.SetBool(disappearBoolName, true); // Activer le booléen pour l'animation de disparition
    }

    // Fonction pour désactiver l'objet
    void Disappear()
    {
        animator.SetBool(touchBoolName, false); // Désactiver le booléen pour l'animation de toucher
        animator.SetBool(disappearBoolName, false); // Désactiver le booléen pour l'animation de disparition
        gameObject.SetActive(false); // Désactiver l'objet
        isDisabled = true; // Marquer l'objet comme désactivé
        Invoke("Reappear", reappearDelay); // Programmer la réapparition de l'objet après le délai spécifié
    }

    // Fonction pour réactiver l'objet
    void Reappear()
    {
        gameObject.SetActive(true); // Réactiver l'objet
        isDisabled = false; // Marquer l'objet comme activé
    }
}
