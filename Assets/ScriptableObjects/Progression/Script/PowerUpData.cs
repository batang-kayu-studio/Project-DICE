using UnityEngine;

/// <summary>
/// Data for one power up. Create one asset per power up
/// (right-click in Project -> Create -> Power Up -> Power Up Data).
/// </summary>
[CreateAssetMenu(fileName = "New Power Up", menuName = "Power Up/Power Up Data")]
public class PowerUpData : ScriptableObject
{
    public enum PowerUpCategory
    {
        Normal,
        BossOnly
    }

    [Header("Display")]
    public Sprite icon;
    [TextArea(2, 4)]
    public string description;

    [Header("Availability")]
    public PowerUpCategory category = PowerUpCategory.Normal;
}