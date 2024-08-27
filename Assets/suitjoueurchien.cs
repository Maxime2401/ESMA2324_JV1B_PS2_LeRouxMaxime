using UnityEngine;

public class FollowPlayerSmooth : MonoBehaviour
{
    public Transform joueur; // Référence au joueur que le chien va suivre
    public float vitesseSuivi = 5f; // Vitesse de suivi (plus élevé = plus rapide)
    public float distanceMinimale = 1f; // Distance minimale entre le joueur et le chien
    public float lissageMouvement = 0.125f; // Facteur de lissage du mouvement (plus petit = plus lisse)

    private Vector3 vitesse = Vector3.zero;

    void Update()
    {
        if (joueur != null)
        {
            // Calculer la distance entre le chien et le joueur
            float distanceAuJoueur = Vector2.Distance(transform.position, joueur.position);

            // Si la distance est supérieure à la distance minimale, déplacer le chien vers le joueur
            if (distanceAuJoueur > distanceMinimale)
            {
                // Position cible pour le chien (à l'endroit du joueur)
                Vector3 positionCible = joueur.position;

                // Lisser le mouvement vers la position cible
                transform.position = Vector3.SmoothDamp(transform.position, positionCible, ref vitesse, lissageMouvement, vitesseSuivi, Time.deltaTime);
            }
        }
    }
}
