using UnityEngine;
using UnityEngine.UI;

public class HealthSpriteUpdater : MonoBehaviour
{
    public string sliderObjectName = "HealthSlider"; // Nom de l'objet portant le Slider
    private Slider healthSlider; // Référence au Slider représentant la santé du joueur
    public Image healthImage; // Référence à l'Image UI contenant les sprites de santé

    public Sprite fullHealthSprite; // Sprite à utiliser lorsque la santé est pleine
    public Sprite mediumHealthSprite; // Sprite à utiliser lorsque la santé est moyenne
    public Sprite lowHealthSprite; // Sprite à utiliser lorsque la santé est faible

    void Start()
    {
        // Trouver le Slider par son nom
        GameObject sliderObject = GameObject.Find(sliderObjectName);
        if (sliderObject != null)
        {
            healthSlider = sliderObject.GetComponent<Slider>();
            if (healthSlider == null)
            {
                Debug.LogError("HealthSlider object does not contain a Slider component.");
            }
        }
        else
        {
            Debug.LogError("No object found with the name " + sliderObjectName);
        }
    }

    // Met à jour le sprite en fonction du pourcentage de santé
    public void UpdateSprite()
    {
        if (healthSlider != null)
        {
            float healthPercentage = healthSlider.value / healthSlider.maxValue * 100f; // Calcul du pourcentage de santé
            if (healthPercentage >= 66f)
            {
                healthImage.sprite = fullHealthSprite;
            }
            else if (healthPercentage >= 33f)
            {
                healthImage.sprite = mediumHealthSprite;
            }
            else
            {
                healthImage.sprite = lowHealthSprite;
            }
        }
        else
        {
            Debug.LogError("HealthSlider reference is null. Make sure to set the sliderObjectName or assign the HealthSlider object manually.");
        }
    }
}
