using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A single equipped power up icon, with a background frame behind the icon
/// (since power up icon PNGs are transparent). Clicking it shows its
/// description via the PowerUpEquipmentManager that instantiated it.
/// Assumes all power up icons are pre-sliced to 1:1 aspect ratio.
/// </summary>
[RequireComponent(typeof(Button))]
public class PowerUpDescDisplay : MonoBehaviour
{
    [Tooltip("The child Image that displays the power up's icon (on top of the background)")]
    [SerializeField] private Image iconImage;

    private PowerUpData data;
    private PowerUpEquipmentManager manager;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    /// <summary>True if this slot already has a power up equipped.</summary>
    public bool IsEquipped => data != null;

    public void Setup(PowerUpData powerUpData, PowerUpEquipmentManager owningManager)
    {
        data = powerUpData;
        manager = owningManager;
        iconImage.sprite = data.icon;
        iconImage.enabled = true;
    }

    private void OnClicked()
    {
        if (IsEquipped)
        {
            manager.ShowDescription(data);
        }
        else
        {
            manager.OnEmptySlotClicked(this);
        }
    }
}