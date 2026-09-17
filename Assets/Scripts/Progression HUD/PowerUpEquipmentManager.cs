using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages equipping power ups to dice faces using pre-placed slot GameObjects
/// already built into the prefab (Power Up 1/2/3 under each face), rather than
/// instantiating new ones. Slot count varies per face:
/// Face 1 & 2 = 3 slots, Face 3 & 4 = 2 slots, Face 5 & 6 = 1 slot.
/// </summary>
public class PowerUpEquipmentManager : MonoBehaviour
{
    [Serializable]
    public class FaceSlots
    {
        [Tooltip("Drag this face's Power Up N GameObjects here, in order. Only as many as this face allows.")]
        public PowerUpDescDisplay[] slots;
    }

    [Header("Power Up Database")]
    [Tooltip("Drag all PowerUpData assets here")]
    [SerializeField] private List<PowerUpData> allPowerUps;

    [Header("Dice Face Slots")]
    [Tooltip("Index 0 = face value 1, index 1 = face value 2, ... index 5 = face value 6")]
    [SerializeField] private FaceSlots[] faces = new FaceSlots[6];

    [Header("Icon & Description Display")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI descriptionText;

    /// <summary>
    /// Equips a random power up from the database into the next empty slot
    /// on the given dice face. Does nothing (logs a warning) if that face is full.
    /// </summary>
    public void EquipRandomPowerUp(int faceIndex)
    {
        if (faceIndex < 0 || faceIndex >= faces.Length || faces[faceIndex] == null)
        {
            Debug.LogWarning($"[PowerUpEquipmentManager] Invalid face index: {faceIndex}");
            return;
        }

        if (allPowerUps == null || allPowerUps.Count == 0)
        {
            Debug.LogWarning("[PowerUpEquipmentManager] No power ups in the database to equip.");
            return;
        }

        PowerUpDescDisplay targetSlot = FindNextEmptySlot(faceIndex);
        if (targetSlot == null)
        {
            Debug.LogWarning($"[PowerUpEquipmentManager] Face {faceIndex} has no empty slots left.");
            return;
        }

        PowerUpData randomPowerUp = allPowerUps[UnityEngine.Random.Range(0, allPowerUps.Count)];
        targetSlot.Setup(randomPowerUp, this);
    }

    private PowerUpDescDisplay FindNextEmptySlot(int faceIndex)
    {
        foreach (PowerUpDescDisplay slot in faces[faceIndex].slots)
        {
            if (slot != null && !slot.IsEquipped)
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>Called by PowerUpDescDisplay when a power up icon is clicked.</summary>
    public void ShowDescription(PowerUpData data)
    {
        iconImage.sprite = data.icon;
        iconImage.enabled = true;
        descriptionText.text = data.description;
    }
}