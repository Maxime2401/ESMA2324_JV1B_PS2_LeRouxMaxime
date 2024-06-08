using UnityEngine;

public class ActiverDesactiverUIAvecTouche : MonoBehaviour
{
    public GameObject elementUI; // L'élément UI à afficher ou masquer lorsque le joueur appuie sur la touche
    public KeyCode toucheActivation = KeyCode.E; // La touche à appuyer pour afficher ou masquer l'UI

    private bool joueurAInterieur = false; // Pour suivre si le joueur est à l'intérieur du déclencheur

    private void Start()
    {
        // Assurez-vous que l'élément UI est initialement désactivé
        if (elementUI != null)
        {
            elementUI.SetActive(false);
        }
    }

    private void Update()
    {
        // Vérifiez si le joueur est à l'intérieur du déclencheur et si la touche spécifiée est enfoncée
        if (joueurAInterieur && Input.GetKeyDown(toucheActivation))
        {
            if (elementUI != null)
            {
                elementUI.SetActive(!elementUI.activeSelf); // Basculer l'état actif de l'élément UI
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D autre)
    {
        if (autre.CompareTag("Player"))
        {
            joueurAInterieur = true; // Indique que le joueur est à l'intérieur du déclencheur
        }
    }

    private void OnTriggerExit2D(Collider2D autre)
    {
        if (autre.CompareTag("Player"))
        {
            elementUI.SetActive(false); // Indique que le joueur n'est plus à l'intérieur du déclencheur
        }
    }
}
