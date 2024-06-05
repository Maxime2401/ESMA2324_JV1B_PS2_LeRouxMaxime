using UnityEngine;

public class BlinkingSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private bool isVisible = true;

    private void Start()
    {
        // Start blinking when this script is enabled
        StartBlinking();
    }

    private void Update()
    {
        // Toggle visibility of the sprite renderer every 1 second
        if (Time.time % 1.0f < 0.5f)
        {
            spriteRenderer.enabled = isVisible;
        }
        else
        {
            spriteRenderer.enabled = !isVisible;
        }
    }

    public void StartBlinking()
    {
        // Ensure the sprite renderer component is assigned
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void StopBlinking()
    {
        // Ensure the sprite renderer is enabled
        spriteRenderer.enabled = true;
    }
}
