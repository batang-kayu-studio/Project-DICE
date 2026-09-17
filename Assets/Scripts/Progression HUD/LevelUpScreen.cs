using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level Up screen: displays 3 randomly chosen, non-repeating power ups.
/// Clicking one stores it as "pending" on PowerUpEquipmentManager and opens
/// the Equipment screen so the player can pick which dice face to put it on.
/// </summary>
public class LevelUpScreen : MonoBehaviour
{
    [Header("Power Up Pool")]
    [Tooltip("The power ups this Level Up screen can offer. Drag in whichever set applies (e.g. Normal-only for a regular level up).")]
    [SerializeField] private List<PowerUpData> availablePowerUps;

    [Header("Option Rows")]
    [Tooltip("The 3 row GameObjects, each with a LevelUpOption component")]
    [SerializeField] private LevelUpOption[] optionSlots;

    [Header("Next Screen")]
    [SerializeField] private PowerUpsEquipmentScreen powerUpEquipmentScreen;
    [SerializeField] private PowerUpEquipmentManager powerUpEquipmentManager;

    /// <summary>Call this to open the Level Up screen with a fresh set of random options.</summary>
    public void Show()
    {
        List<PowerUpData> picks = PickRandomUnique(availablePowerUps, optionSlots.Length);

        for (int i = 0; i < optionSlots.Length; i++)
        {
            if (i < picks.Count)
            {
                optionSlots[i].gameObject.SetActive(true);
                optionSlots[i].Setup(picks[i], this);
            }
            else
            {
                // pool had fewer entries than rows; hide the leftover row
                optionSlots[i].gameObject.SetActive(false);
            }
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>Called by a LevelUpOption when the player clicks it.</summary>
    public void SelectPowerUp(PowerUpData data)
    {
        powerUpEquipmentManager.SetPendingPowerUp(data);
        Hide();
        powerUpEquipmentScreen.Show();
    }

    private List<PowerUpData> PickRandomUnique(List<PowerUpData> source, int count)
    {
        List<PowerUpData> remaining = new List<PowerUpData>(source);
        List<PowerUpData> result = new List<PowerUpData>();

        for (int i = 0; i < count && remaining.Count > 0; i++)
        {
            int index = Random.Range(0, remaining.Count);
            result.Add(remaining[index]);
            remaining.RemoveAt(index);
        }

        return result;
    }
}