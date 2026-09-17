using UnityEngine;

/// <summary>
/// Test-only script to trigger EquipRandomPowerUp on a chosen face from the Inspector.
/// Attach to the same GameObject as PowerUpEquipmentManager.
/// </summary>
[RequireComponent(typeof(PowerUpEquipmentManager))]
public class PowerUpEquipmentTester : MonoBehaviour
{
    [Tooltip("0 = face value 1, 1 = face value 2, ... 5 = face value 6")]
    [SerializeField] private int testFaceIndex = 0;

    private PowerUpEquipmentManager manager;

    void Awake()
    {
        manager = GetComponent<PowerUpEquipmentManager>();
    }

    [ContextMenu("Equip Random Power Up To Selected Face")]
    public void TestEquipRandom()
    {
        manager.EquipRandomPowerUp(testFaceIndex);
    }
}