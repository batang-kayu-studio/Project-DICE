using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Drives an enemy health bar: red fill clipped inside a frame, with the
/// current HP value centered as text. Call SetHealth() whenever health changes.
/// </summary>
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI valueText;

    [Header("Fill animation")]
    [SerializeField] private float fillLerpDuration = 0.3f;

    private int maxHealth;
    private Coroutine fillRoutine;

    /// <summary>Call this once when the enemy spawns, to set its max health.</summary>
    public void Initialize(int max)
    {
        maxHealth = max;
        fillImage.fillAmount = 1f;
        UpdateText(max);
    }

    /// <summary>Call this whenever the enemy takes damage or heals.</summary>
    public void SetHealth(int current)
    {
        current = Mathf.Clamp(current, 0, maxHealth);
        float targetFill = maxHealth > 0 ? (float)current / maxHealth : 0f;

        UpdateText(current);

        if (fillRoutine != null) StopCoroutine(fillRoutine);
        fillRoutine = StartCoroutine(LerpFill(targetFill));
    }

    private void UpdateText(int current)
    {
        valueText.text = $"{current}/{maxHealth}";
    }

    private IEnumerator LerpFill(float target)
    {
        float start = fillImage.fillAmount;
        float t = 0f;

        while (t < fillLerpDuration)
        {
            t += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(start, target, t / fillLerpDuration);
            yield return null;
        }

        fillImage.fillAmount = target;
    }
}