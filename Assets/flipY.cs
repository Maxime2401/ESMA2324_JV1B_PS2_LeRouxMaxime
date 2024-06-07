using UnityEngine;

public class FlipOnYMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool goingDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goingDown = false;
    }

    void Update()
    {
        // Vérifier la direction du mouvement sur l'axe y
        if (rb.velocity.y < -0.1f)
        {
            goingDown = true;
        }
        else if (rb.velocity.y > 0.1f)
        {
            goingDown = false;
        }

        // Flipping de l'échelle en fonction de la direction
        if (goingDown)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
