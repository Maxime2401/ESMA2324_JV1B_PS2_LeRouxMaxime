using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Le préfab de projectile à tirer
    public float projectileSpeed = 10f; // La vitesse du projectile
    public Transform shootPoint; // Le point de tir (où le projectile apparaîtra)

    void Update()
    {
        // Détecter l'entrée du joueur pour tirer (par exemple, en appuyant sur la barre d'espace)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Créer une instance du projectile au point de tir
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Ajouter une force vers le haut au projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.up * projectileSpeed;
        }
    }
}
