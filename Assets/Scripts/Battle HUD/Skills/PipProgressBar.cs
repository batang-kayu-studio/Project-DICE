using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Turn-meter style progress bar made of individual pips (e.g. "swipes remaining").
/// Instantiate the correct number of pips at spawn via Initialize(), then call
/// SetFilled() each time progress advances to swap pip sprites off->on in order.
/// The container GameObject should have a Grid Layout Group with a Fixed Column
/// Count constraint set, so pips automatically wrap to a new row past that count.
/// </summary>
public class PipProgressBar : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Prefab with an Image component, no sprite needed on it")]
    [SerializeField] private GameObject pipPrefab;
    [Tooltip("The GameObject with the Grid Layout Group. Defaults to this object if left empty.")]
    [SerializeField] private Transform pipContainer;
    [SerializeField] private Sprite pipOffSprite;
    [SerializeField] private Sprite pipOnSprite;

    private readonly List<Image> pipImages = new List<Image>();

    void Awake()
    {
        if (pipContainer == null)
        {
            pipContainer = transform;
        }
    }

    /// <summary>
    /// Builds the bar with the given number of pips, all starting in the "off" state.
    /// Call this once when the bar's total count is known (e.g. enemy's turn icons required).
    /// </summary>
    public void Initialize(int totalPips)
    {
        // clear any pips from a previous use
        foreach (Transform child in pipContainer)
        {
            Destroy(child.gameObject);
        }
        pipImages.Clear();

        for (int i = 0; i < totalPips; i++)
        {
            GameObject pipObj = Instantiate(pipPrefab, pipContainer);
            Image img = pipObj.GetComponent<Image>();
            img.sprite = pipOffSprite;
            pipImages.Add(img);
        }
    }

    /// <summary>
    /// Sets how many pips (from the start) should be "on". Call this whenever
    /// progress changes, e.g. once per swipe.
    /// </summary>
    public void SetFilled(int filledCount)
    {
        filledCount = Mathf.Clamp(filledCount, 0, pipImages.Count);

        for (int i = 0; i < pipImages.Count; i++)
        {
            pipImages[i].sprite = i < filledCount ? pipOnSprite : pipOffSprite;
        }
    }
}