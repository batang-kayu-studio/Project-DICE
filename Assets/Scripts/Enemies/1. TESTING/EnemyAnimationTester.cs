using UnityEngine;

/// <summary>
/// Test-only script to trigger the enemy's Attack and Death animations from the
/// Inspector while playing, without needing gameplay logic wired up yet.
/// Attach to the enemy Prefab alongside its Animator.
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimationTester : MonoBehaviour
{
    [Tooltip("Must match the Trigger parameter name in the Animator Controller")]
    [SerializeField] private string attackTriggerName = "Attack";

    [Tooltip("Must match the Trigger parameter name in the Animator Controller")]
    [SerializeField] private string deathTriggerName = "Death";

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Test Attack Animation")]
    public void TestAttack()
    {
        animator.SetTrigger(attackTriggerName);
    }

    [ContextMenu("Test Death Animation")]
    public void TestDeath()
    {
        animator.SetTrigger(deathTriggerName);
    }
}