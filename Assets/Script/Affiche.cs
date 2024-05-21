using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    public Image imageToDisplay; // Référence à l'image que vous souhaitez afficher
    public Image imageToDisplay2;
    public void ToggleImage()
    {
        // Vérifiez si l'image est actuellement activée ou désactivée
        bool imageActive = imageToDisplay.gameObject.activeSelf;

        // Activez ou désactivez l'image en fonction de son état actuel
        imageToDisplay.gameObject.SetActive(!imageActive);

    }
}
