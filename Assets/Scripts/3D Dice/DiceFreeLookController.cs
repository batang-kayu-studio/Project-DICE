using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages switching the dice between "Free Look" mode (player can drag to inspect
/// any face) and "Swipe Ready" mode. On exit, the dice animates back to EXACTLY the
/// rotation it had the moment free look was entered — not just any valid grid pose —
/// so the player's actual gameplay-facing face never changes just from inspecting.
///
/// Wire this up with two UI Buttons:
///   - "Inspect" button -> OnClick -> EnterFreeLook()
///   - "Ready" button   -> OnClick -> ExitFreeLook()
///
/// Hook onEnterFreeLook / onExitFreeLook in the Inspector to disable/enable
/// your swipe input script, so this stays decoupled from however you build that.
/// </summary>
[RequireComponent(typeof(DiceDragRotate))]
public class DiceFreeLookController : MonoBehaviour
{
    [Header("Snap-back settings")]
    [Tooltip("How long the return-to-original-rotation animation takes when exiting free look")]
    [SerializeField] private float snapDuration = 0.3f;
    [SerializeField] private AnimationCurve snapEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Events (hook up your swipe script here)")]
    public UnityEvent onEnterFreeLook;
    public UnityEvent onExitFreeLook; // invoked AFTER the snap-back animation finishes

    [Header("Debug (read-only, for testing in Play Mode)")]
    [SerializeField, Tooltip("Current state, updates live in the Inspector while playing")]
    private string currentState = "Locked";

    private DiceDragRotate dragRotate;
    private bool isSnapping;

    // The exact rotation the dice had right before free look was entered.
    // This is what we animate back to on exit.
    private Quaternion rotationBeforeFreeLook;

    public bool IsFreeLookActive => dragRotate.enabled;

    void Awake()
    {
        dragRotate = GetComponent<DiceDragRotate>();
        dragRotate.enabled = false; // start locked, not draggable, until player taps Inspect
    }

    void Update()
    {
        currentState = isSnapping ? "Returning..." : (dragRotate.enabled ? "Free Look Active" : "Locked");
    }

    /// <summary>
    /// Call from your "Inspect" button's OnClick.
    /// TESTING: during Play Mode, right-click this component's header in the
    /// Inspector (or click the gear/⋮ icon) and choose "Enter Free Look" to
    /// trigger this without needing a UI button wired up yet.
    /// </summary>
    [ContextMenu("Enter Free Look")]
    public void EnterFreeLook()
    {
        if (isSnapping) return;

        rotationBeforeFreeLook = transform.rotation; // remember exactly where we started
        dragRotate.enabled = true;
        onEnterFreeLook?.Invoke();
    }

    /// <summary>
    /// Call from your "Ready" button's OnClick.
    /// TESTING: during Play Mode, right-click this component's header in the
    /// Inspector (or click the gear/⋮ icon) and choose "Exit Free Look" to
    /// trigger this without needing a UI button wired up yet.
    /// </summary>
    [ContextMenu("Exit Free Look")]
    public void ExitFreeLook()
    {
        if (isSnapping) return;
        dragRotate.enabled = false;
        StartCoroutine(ReturnToOriginalRotation());
    }

    private IEnumerator ReturnToOriginalRotation()
    {
        isSnapping = true;

        Quaternion start = transform.rotation;
        Quaternion target = rotationBeforeFreeLook;

        float t = 0f;
        while (t < snapDuration)
        {
            t += Time.deltaTime;
            float eased = snapEase.Evaluate(Mathf.Clamp01(t / snapDuration));
            transform.rotation = Quaternion.Slerp(start, target, eased);
            yield return null;
        }

        transform.rotation = target; // guarantee exact final orientation, no drift
        isSnapping = false;
        onExitFreeLook?.Invoke();
    }
}