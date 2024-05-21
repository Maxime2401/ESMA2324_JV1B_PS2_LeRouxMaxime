using UnityEngine;

public class Swimming : MonoBehaviour
{
    public float swimSpeed = 2f; // Vitesse de nage
    public float riseSpeed = 1f; // Vitesse de montée
    public float descentSpeed = 1f; // Vitesse de descente

    private bool isSwimming = false; // Indique si le personnage est en train de nager

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isSwimming = true; // Activer le mode de nage lorsque le personnage entre dans l'eau
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isSwimming = false; // Désactiver le mode de nage lorsque le personnage quitte l'eau
        }
    }

    void Update()
    {
        if (isSwimming)
        {
            // Gérer les contrôles de nage ici
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 swimDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            transform.Translate(swimDirection * swimSpeed * Time.deltaTime);

            // Gérer la montée et la descente dans l'eau
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.down * descentSpeed * Time.deltaTime);
            }
        }
    }
}
