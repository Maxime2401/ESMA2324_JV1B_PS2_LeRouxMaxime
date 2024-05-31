using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image imageToShow; // Référence à l'image à afficher
    public AudioClip soundToPlay; // Référence au son à jouer

    private AudioSource audioSource; // Référence au composant AudioSource

    // Initialisation
    void Start()
    {
        // Récupérer le composant AudioSource attaché à cet objet ou à un parent
        audioSource = GetComponentInParent<AudioSource>();

        // Vérifier si un AudioSource existe
        if (audioSource == null)
        {
            Debug.LogWarning("Pas de composant AudioSource trouvé. Assurez-vous d'avoir attaché un AudioSource à cet objet ou à un parent.");
        }
    }

    // Méthode appelée lorsque le curseur entre dans le bouton
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Activer l'image à afficher
        imageToShow.gameObject.SetActive(true);

        // Jouer le son
        if (audioSource != null && soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }

    // Méthode appelée lorsque le curseur quitte le bouton
    public void OnPointerExit(PointerEventData eventData)
    {
        // Désactiver l'image
        imageToShow.gameObject.SetActive(false);
    }
}
