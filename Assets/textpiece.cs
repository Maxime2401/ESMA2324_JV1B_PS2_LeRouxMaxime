using UnityEngine;
using UnityEngine.UI;

public class TextePiecess : MonoBehaviour
{
    public Text texte;

    // Méthode pour mettre à jour le texte avec le nombre de pièces
    public void MettreAJourTexte(int nombrePieces)
    {
        texte.text = "Pièces : " + nombrePieces.ToString();
    }

    // Cette méthode est appelée lorsque le GameObject est activé
    private void OnEnable()
    {
        // Assurez-vous que le GameObject n'est pas détruit lorsque la scène est changée
        DontDestroyOnLoad(gameObject);
    }
}