using UnityEngine;

/// <summary>
/// Shows/hides the Power Ups Equipment overlay screen (panel + dim background).
/// Attach to the root of the PowerUpsEquipmentScreen prefab instance.
/// Wire your button's OnClick to Toggle().
/// </summary>
public class PowerUpsEquipmentScreen : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}