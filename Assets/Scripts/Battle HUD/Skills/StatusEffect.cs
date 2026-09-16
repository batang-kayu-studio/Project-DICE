using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays a single status effect's icon and value. Instantiated as needed
/// into SkillDisplay's status effect container.
/// </summary>
public class StatusEffect : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI valueText;

    public void SetData(Sprite icon, int value)
    {
        iconImage.sprite = icon;
        valueText.text = value.ToString();
    }
}