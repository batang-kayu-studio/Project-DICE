using UnityEngine;

/// <summary>
/// Test-only script to manipulate EnemyHealthBar's values from the Inspector,
/// standing in for a real HealthController until that's built.
/// Attach to the same GameObject as EnemyHealthBar.
/// </summary>
[RequireComponent(typeof(EnemyHealthBar))]
public class EnemyHealthBarTester : MonoBehaviour
{
    [Header("Test values (edit these, then use the buttons below)")]
    [SerializeField] private int testMaxHealth = 30;
    [SerializeField] private int testCurrentHealth = 30;

    private EnemyHealthBar healthBar;

    void Awake()
    {
        healthBar = GetComponent<EnemyHealthBar>();
    }

    [ContextMenu("1. Initialize (spawn with Max Health)")]
    public void TestInitialize()
    {
        testCurrentHealth = testMaxHealth;
        healthBar.Initialize(testMaxHealth);
    }

    [ContextMenu("2. Apply Current Health")]
    public void TestApplyCurrentHealth()
    {
        healthBar.SetHealth(testCurrentHealth);
    }
}