using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dammage : MonoBehaviour
{
    // Nombre de dégâts infligés au joueur
    public int damageAmount = 4;

    // Fonction OnTriggerEnter2D pour détecter les collisions avec le joueur et les triggers électriques
    void OnTriggerEnter2D(Collider2D other)
    {   

        // Si la collision est avec un joueur
        if (other.CompareTag("Player"))
        {
            barvi barvi = other.GetComponent<barvi>();

            // Si le composant barvi existe sur le joueur
            if (barvi != null)
            {
                // Infligez des dégâts au joueur
                barvi.TakeDamage(damageAmount);
                Debug.Log("Damage dealt to player: " + damageAmount);
            }
        }
    }
}
