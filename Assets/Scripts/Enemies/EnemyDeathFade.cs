using System.Collections;
using UnityEngine;

/// <summary>
/// Fades this enemy's sprite opacity to zero. Call TriggerDeathFade() via an
/// Animation Event placed on the last frame of the Death animation clip.
/// </summary>
public class EnemyDeathFade : MonoBehaviour
{
    [Tooltip("Auto-found via GetComponentInChildren if left empty")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float fadeDuration = 1f;

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    /// <summary>Called via Animation Event on the Death clip's last frame.</summary>
    public void TriggerDeathFade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color startColor = spriteRenderer.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, t / fadeDuration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}