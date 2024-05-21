using UnityEngine;
using System.Collections;

public class SmoothDisappearReappear : MonoBehaviour
{
    public float minDisappearTime = 1f; // Temps minimum avant la disparition
    public float maxDisappearTime = 3f; // Temps maximum avant la disparition
    public float minReappearTime = 1f; // Temps minimum avant la réapparition
    public float maxReappearTime = 3f; // Temps maximum avant la réapparition
    public float fadeDuration = 0.5f; // Durée du fondu
    public float smoothSpeed = 5f; // Vitesse du fondu

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine disappearCoroutine;
    private Coroutine reappearCoroutine;
    private Vector3 initialPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        initialPosition = transform.position;

        // Démarrer la routine de disparition
        StartDisappearRoutine();
    }

    IEnumerator Disappear()
    {
        float timeBeforeDisappear = Random.Range(minDisappearTime, maxDisappearTime);
        yield return new WaitForSeconds(timeBeforeDisappear);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime * smoothSpeed;
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Démarrer la routine de réapparition
        StartReappearRoutine();
    }

    IEnumerator Reappear()
    {
        float timeBeforeReappear = Random.Range(minReappearTime, maxReappearTime);
        yield return new WaitForSeconds(timeBeforeReappear);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime * smoothSpeed;
            yield return null;
        }

        spriteRenderer.color = originalColor;

        // Réinitialiser la position de l'objet à sa position initiale
        transform.position = initialPosition;

        // Démarrer la routine de disparition
        StartDisappearRoutine();
    }

    void StartDisappearRoutine()
    {
        if (disappearCoroutine != null)
            StopCoroutine(disappearCoroutine);
        disappearCoroutine = StartCoroutine(Disappear());
    }

    void StartReappearRoutine()
    {
        if (reappearCoroutine != null)
            StopCoroutine(reappearCoroutine);
        reappearCoroutine = StartCoroutine(Reappear());
    }
}
