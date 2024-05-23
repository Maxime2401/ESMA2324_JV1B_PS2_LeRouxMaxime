using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;


    void Start()
    {
        // Charger les paramètres sauvegardés
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        // Ajouter des listeners pour sauvegarder les paramètres lorsque modifiés
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Ajuster le volume du jeu
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
