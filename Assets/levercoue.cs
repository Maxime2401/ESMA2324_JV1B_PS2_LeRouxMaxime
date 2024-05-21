using UnityEngine;

public class cricou : MonoBehaviour
{
    public Transform cou;
    public float angleLeve = 45f;
    public float angleMilieu = 0f; // Nouvelle position intermédiaire
    public float angleBaisse = -45f;
    public float vitesse = 5f;

    private float angleCible;
    private int etatCou; // 0: baissé, 1: milieu, 2: levé

    void Start()
    {
        etatCou = 0; // Commence avec le cou baissé
        angleCible = angleBaisse;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MonterCou();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DescendreCou();
        }

        // Smooth transition
        float angleActuel = Mathf.LerpAngle(cou.localEulerAngles.z, angleCible, Time.deltaTime * vitesse);
        cou.localEulerAngles = new Vector3(0, 0, angleActuel);
    }

    void MonterCou()
    {
        if (etatCou < 2)
        {
            etatCou++;
            MettreAJourAngleCible();
        }
    }

    void DescendreCou()
    {
        if (etatCou > 0)
        {
            etatCou--;
            MettreAJourAngleCible();
        }
    }

    void MettreAJourAngleCible()
    {
        switch (etatCou)
        {
            case 0:
                angleCible = angleBaisse;
                break;
            case 1:
                angleCible = angleMilieu;
                break;
            case 2:
                angleCible = angleLeve;
                break;
        }
    }
}
