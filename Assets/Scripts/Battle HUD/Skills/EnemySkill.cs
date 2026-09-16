using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays a skill: a main icon+value that switches between Attack/Heal,
/// plus an optional row of status effect icons (burn, heal over time, etc.)
/// instantiated as needed.
/// </summary>
public class EnemySkill : MonoBehaviour
{
    [Header("Main Skill")]
    [SerializeField] private Image mainIcon;
    [SerializeField] private TextMeshProUGUI mainValueText;
    [SerializeField] private Sprite attackIconSprite;
    [SerializeField] private Sprite healIconSprite;

    [Header("Status Effects")]
    [Tooltip("Prefab with a StatusEffect component")]
    [SerializeField] private GameObject statusEffectPrefab;
    [Tooltip("The GameObject status effect icons get instantiated into")]
    [SerializeField] private Transform statusEffectContainer;

    private readonly List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    public void SetAttack(int value)
    {
        mainIcon.sprite = attackIconSprite;
        mainValueText.text = value.ToString();
    }

    public void SetHeal(int value)
    {
        mainIcon.sprite = healIconSprite;
        mainValueText.text = value.ToString();
    }

    /// <summary>Instantiates a new status effect icon (e.g. burn, heal over time) with the given icon and value.</summary>
    public void AddStatusEffect(Sprite statusIcon, int value)
    {
        GameObject obj = Instantiate(statusEffectPrefab, statusEffectContainer);
        StatusEffect icon = obj.GetComponent<StatusEffect>();
        icon.SetData(statusIcon, value);
        activeStatusEffects.Add(icon);
    }

    public void ClearStatusEffects()
    {
        foreach (StatusEffect effect in activeStatusEffects)
        {
            if (effect != null) Destroy(effect.gameObject);
        }
        activeStatusEffects.Clear();
    }
}