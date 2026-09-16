using UnityEngine;

/// <summary>
/// Test-only script to manipulate SkillDisplay from the Inspector,
/// standing in for real game logic until that's built.
/// Attach to the same GameObject as SkillDisplay.
/// </summary>
[RequireComponent(typeof(EnemySkill))]
public class EnemySkillTester : MonoBehaviour
{
    [Header("Main Skill test values")]
    [SerializeField] private int testAttackValue = 5;
    [SerializeField] private int testHealValue = 8;

    [Header("Status Effect test values")]
    [SerializeField] private Sprite burnIconSprite;
    [SerializeField] private int testBurnValue = 3;
    [SerializeField] private Sprite healOverTimeIconSprite;
    [SerializeField] private int testHealOverTimeValue = 2;

    private EnemySkill enemySkill;

    void Awake()
    {
        enemySkill = GetComponent<EnemySkill>();
    }

    [ContextMenu("Set Attack")]
    public void TestSetAttack()
    {
        enemySkill.SetAttack(testAttackValue);
    }

    [ContextMenu("Set Heal")]
    public void TestSetHeal()
    {
        enemySkill.SetHeal(testHealValue);
    }

    [ContextMenu("Add Burn Status Effect")]
    public void TestAddBurn()
    {
        enemySkill.AddStatusEffect(burnIconSprite, testBurnValue);
    }

    [ContextMenu("Add Heal Over Time Status Effect")]
    public void TestAddHealOverTime()
    {
        enemySkill.AddStatusEffect(healOverTimeIconSprite, testHealOverTimeValue);
    }

    [ContextMenu("Clear Status Effects")]
    public void TestClearStatusEffects()
    {
        enemySkill.ClearStatusEffects();
    }
}