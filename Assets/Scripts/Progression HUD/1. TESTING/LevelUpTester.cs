using UnityEngine;

/// <summary>
/// Test-only script to show/hide the Level Up screen from the Inspector,
/// standing in for real "player leveled up" trigger logic until that's built.
/// Attach to the same GameObject as LevelUpScreen.
/// </summary>
[RequireComponent(typeof(LevelUpScreen))]
public class LevelUpTester : MonoBehaviour
{
    private LevelUpScreen levelUpScreen;

    void Awake()
    {
        levelUpScreen = GetComponent<LevelUpScreen>();
    }

    [ContextMenu("Show Level Up Screen")]
    public void TestShow()
    {
        levelUpScreen.Show();
    }

    [ContextMenu("Hide Level Up Screen")]
    public void TestHide()
    {
        levelUpScreen.Hide();
    }
}