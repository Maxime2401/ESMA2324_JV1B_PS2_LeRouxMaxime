using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float flightDuration = 5f; // Durée de vol en secondes
    public Slider flightDurationSlider; // Référence au slider
    public Vector3 sliderOffset; // Offset pour positionner le slider au-dessus du joueur

    private float flightTimeElapsed = 0f; // Temps de vol écoulé
    private bool canFly = true; // Indique si le joueur peut voler

    void Start()
    {
        // Initialiser le slider
        if (flightDurationSlider != null)
        {
            flightDurationSlider.minValue = 0;
            flightDurationSlider.maxValue = flightDuration;
            flightDurationSlider.value = flightDuration;
            flightDurationSlider.gameObject.SetActive(false); // Masquer le slider au début
        }
    }

    void Update()
    {
        // Contrôles de déplacement avec les flèches directionnelles
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcul du vecteur de déplacement
        Vector3 movement = new Vector3(horizontalInput, canFly ? verticalInput : 0f, 0f) * moveSpeed * Time.deltaTime;

        // Appliquer le déplacement à la position de l'objet
        transform.Translate(movement);

        // Mettre à jour le temps de vol écoulé
        if (canFly)
        {
            flightTimeElapsed += Time.deltaTime;

            // Vérifier si la durée de vol est écoulée
            if (flightTimeElapsed >= flightDuration)
            {
                canFly = false;
            }
        }

        // Mettre à jour la position et la valeur du slider
        UpdateFlightDurationSlider();
        UpdateSliderPosition();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si le joueur touche le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Réinitialiser le temps de vol écoulé et permettre de voler à nouveau
            flightTimeElapsed = 0f;
            canFly = true;

            // Masquer le slider lorsqu'il touche le sol
            if (flightDurationSlider != null)
            {
                flightDurationSlider.gameObject.SetActive(false);
            }
        }
    }

    void UpdateFlightDurationSlider()
    {
        if (flightDurationSlider != null)
        {
            float remainingFlightTime = Mathf.Clamp(flightDuration - flightTimeElapsed, 0, flightDuration);
            flightDurationSlider.value = remainingFlightTime;

            // Afficher le slider lorsque le joueur est en l'air
            if (!canFly)
            {
                flightDurationSlider.gameObject.SetActive(true);
            }
        }
    }

    void UpdateSliderPosition()
    {
        if (flightDurationSlider != null)
        {
            // Obtenir la position du joueur dans l'espace de l'écran
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);

            // Mettre à jour la position du Slider en fonction de la position du joueur
            flightDurationSlider.transform.position = screenPos;

            // Réinitialiser la position en Y pour que le Slider soit au-dessus du joueur
            flightDurationSlider.transform.position = new Vector3(flightDurationSlider.transform.position.x, screenPos.y + 20, flightDurationSlider.transform.position.z);
        }
    }
}
