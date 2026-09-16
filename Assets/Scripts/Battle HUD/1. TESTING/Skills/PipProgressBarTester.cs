using UnityEngine;

/// <summary>
/// Test-only script to manipulate PipProgressBar's values from the Inspector,
/// standing in for real game logic until that's built.
/// Attach to the same GameObject as PipProgressBar (TurnMeterContainer).
/// </summary>
[RequireComponent(typeof(PipProgressBar))]
public class PipProgressBarTester : MonoBehaviour
{
    [Header("Test values (edit these, then use the buttons below)")]
    [SerializeField] private int testTotalPips = 6;
    [SerializeField] private int testFilledCount = 0;

    private PipProgressBar pipBar;

    void Awake()
    {
        pipBar = GetComponent<PipProgressBar>();
    }

    [ContextMenu("1. Initialize (build Total Pips, all off)")]
    public void TestInitialize()
    {
        testFilledCount = 0;
        pipBar.Initialize(testTotalPips);
    }

    [ContextMenu("2. Apply Filled Count")]
    public void TestApplyFilledCount()
    {
        pipBar.SetFilled(testFilledCount);
    }
}