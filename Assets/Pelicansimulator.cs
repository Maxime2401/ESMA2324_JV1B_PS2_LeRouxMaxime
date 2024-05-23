using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public Slider flightDurationSlider; // Référence au slider
    public Vector3 sliderOffset; // Offset pour positionner le slider au-dessus du joueur
    public float rechargeRate = 10f; // Taux de recharge par seconde
    public float maxFlightDuration = 100f; // Durée de vol maximale
    public float drainRate = 20f; // Taux de vidange de la barre de vol lorsque le joueur vole

    private bool isGrounded = false; // Indique si le joueur touche le sol
    private Rigidbody2D rb; // Référence au Rigidbody2D
    private float currentFlightDuration = 0f; // Durée de vol actuelle

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtient le Rigidbody2D
        InitializeFlightDurationSlider();
        ToggleFlightDurationSlider(true); // Activer la barre de durée de vol au démarrage
    }

    void Update()
    {
        if (isGrounded)
        {
            RechargeFlight();
        }
        else
        {
            DrainFlight();
        }

        // Déplacer le joueur de gauche à droite indépendamment de la barre de vol
        HandleHorizontalMovement();

        // Vérifier si le joueur peut voler (durée de vol supérieure à zéro)
        if (currentFlightDuration > 0)
        {
            // Si la durée de vol est supérieure à zéro, le joueur peut voler
            
            HandleVerticalMovement(); // Gérer le mouvement vertical (haut/bas)
        }

        UpdateFlightDurationSlider();
        UpdateSliderPosition();
    }

    void HandleHorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0f) * moveSpeed;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);
    }

    void HandleVerticalMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(0f, verticalInput) * moveSpeed;
        rb.velocity = new Vector2(rb.velocity.x, movement.y);
    }

    void RechargeFlight()
    {
        currentFlightDuration += rechargeRate * Time.deltaTime;
        currentFlightDuration = Mathf.Clamp(currentFlightDuration, 0f, maxFlightDuration);
    }

    void DrainFlight()
    {
        currentFlightDuration -= drainRate * Time.deltaTime;
        currentFlightDuration = Mathf.Clamp(currentFlightDuration, 0f, maxFlightDuration);
    }

    void InitializeFlightDurationSlider()
    {
        if (flightDurationSlider != null)
        {
            flightDurationSlider.minValue = 0;
            flightDurationSlider.maxValue = maxFlightDuration;
            flightDurationSlider.value = currentFlightDuration;
        }
    }

    void UpdateFlightDurationSlider()
    {
        if (flightDurationSlider != null)
        {
            flightDurationSlider.value = currentFlightDuration;
        }
    }

    void UpdateSliderPosition()
    {
        if (flightDurationSlider != null && flightDurationSlider.gameObject.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + sliderOffset);
            flightDurationSlider.transform.position = new Vector3(screenPos.x, screenPos.y + 20, screenPos.z);
        }
    }

    void ToggleFlightDurationSlider(bool isActive)
    {
        if (flightDurationSlider != null)
        {
            flightDurationSlider.gameObject.SetActive(isActive);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnEnable()
    {
        ToggleFlightDurationSlider(true); // Activer la barre de durée de vol lorsque le script est activé
    }

    void OnDisable()
    {
        ToggleFlightDurationSlider(false); // Désactiver la barre de durée de vol lorsque le script est désactivé
    }
}
