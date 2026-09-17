using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// One row option on the Level Up screen: a power up's icon + description.
/// Clicking it selects that power up via the owning LevelUpScreen.
/// </summary>
[RequireComponent(typeof(Button))]
public class LevelUpOption : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI descText;

    private PowerUpData data;
    private LevelUpScreen levelUpScreen;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }

    public void Setup(PowerUpData powerUpData, LevelUpScreen owningScreen)
    {
        data = powerUpData;
        levelUpScreen = owningScreen;
        iconImage.sprite = data.icon;
        iconImage.enabled = true;
        descText.text = data.description;
    }

    private void OnClicked()
    {
        levelUpScreen.SelectPowerUp(data);
    }
}