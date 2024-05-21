using System.Collections;
using UnityEngine;

public class ChargeBehavior : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public GameObject chargeEffect; // GameObject à désactiver pendant la charge
    public float chargeSpeed = 10f; // Vitesse de charge
    public float chargeDuration = 0.5f; // Durée de la charge
    public float chargeCooldown = 2f; // Temps de recharge entre les charges
    public Transform[] teleportPoints; // Points de téléportation après la charge

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private bool isCharging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform; // Trouver le joueur par tag
        }
    }

    void Update()
    {
        if (!isCharging)
        {
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge()
    {
        isCharging = true;

        if (chargeEffect != null)
        {
            chargeEffect.SetActive(false);
        }
        // Désactiver le sprite et le collider avant de charger
        spriteRenderer.enabled = true;
        col.enabled = true;

        // Direction de charge vers le joueur
        Vector2 chargeDirection = (player.position - transform.position).normalized;
        
        // Appliquer la vitesse de charge
        rb.velocity = chargeDirection * chargeSpeed;

        // Désactiver le GameObject d'effet de charge


        // Attendre la durée de la charge
        yield return new WaitForSeconds(chargeDuration); 

        // Stopper la charge
        rb.velocity = Vector2.zero;

        spriteRenderer.enabled = false;
        col.enabled = false;

        if (chargeEffect != null)
        {
            chargeEffect.SetActive(true);
        }

        // Attendre le temps de recharge entre les charges
        yield return new WaitForSeconds(chargeCooldown);

        // Choisir un point de téléportation aléatoire parmi les points disponibles
        if (teleportPoints != null && teleportPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, teleportPoints.Length);
            Transform chosenPoint = teleportPoints[randomIndex];
            
            // Réactiver le sprite et le collider avant de téléporter l'obje

            // Téléporter le GameObject au point de téléportation choisi
            Vector3 newPosition = chosenPoint.position;
            newPosition.z = 0f; // Définir la position Z à 0
            transform.position = newPosition;
        }

        isCharging = false;
    }
}
