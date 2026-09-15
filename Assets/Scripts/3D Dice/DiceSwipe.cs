using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Detects a 4-directional swipe (up/down/left/right) and rotates the dice a clean
/// 90 degrees toward the swiped direction, revealing the adjacent face.
/// Only responds while DiceFreeLookController's free look mode is OFF (i.e. player
/// has tapped "Ready" / hasn't entered inspect mode), so this never fights with
/// free-drag input.
/// </summary>
public class DiceSwipe : MonoBehaviour
{
    [Header("Swipe detection")]
    [Tooltip("Minimum drag distance in pixels before it counts as a swipe")]
    [SerializeField] private float minSwipeDistance = 50f;

    [Header("Rotation animation")]
    [SerializeField] private float rotationDuration = 0.25f;
    [SerializeField] private AnimationCurve rotationEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Optional: link your free look controller")]
    [Tooltip("Auto-found on this GameObject if left empty. Swipes are ignored while its free look is active.")]
    [SerializeField] private DiceFreeLookController freeLookController;

    [Header("Events")]
    [Tooltip("Fires after a swipe rotation finishes. Direction is the swipe direction (Up/Down/Left/Right) as a Vector2, e.g. (0,1) for up.")]
    public UnityEvent<Vector2> onSwipeCompleted;

    private Vector2 pressStartPosition;
    private bool isPressing;
    private bool isRotating;

    void Awake()
    {
        if (freeLookController == null)
        {
            freeLookController = GetComponent<DiceFreeLookController>();
        }
    }

    void Update()
    {
        // Gate: don't process swipes while free look is active, or a swipe is already animating
        if (isRotating) return;
        if (freeLookController != null && freeLookController.IsFreeLookActive) return;

        HandleSwipeInput();
    }

    private void HandleSwipeInput()
    {
        Pointer pointer = Pointer.current;
        if (pointer == null) return;

        if (pointer.press.wasPressedThisFrame)
        {
            isPressing = true;
            pressStartPosition = pointer.position.ReadValue();
        }
        else if (pointer.press.wasReleasedThisFrame && isPressing)
        {
            isPressing = false;
            Vector2 releasePosition = pointer.position.ReadValue();
            Vector2 delta = releasePosition - pressStartPosition;

            if (delta.magnitude >= minSwipeDistance)
            {
                Vector2 direction = GetDominantDirection(delta);
                StartCoroutine(SwipeRotate(direction));
            }
        }
    }

    /// <summary>Snaps the raw delta to one of the 4 cardinal directions.</summary>
    private Vector2 GetDominantDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            return delta.y > 0 ? Vector2.up : Vector2.down;
        }
    }

    private IEnumerator SwipeRotate(Vector2 direction)
    {
        isRotating = true;

        // Map swipe direction to a rotation axis + angle.
        // Swipe up/down tips the dice forward/back (rotate around world Right axis).
        // Swipe left/right tips the dice sideways (rotate around world Up axis).
        // Flip the signs below if the rotation direction feels backwards in playtesting.
        Vector3 axis;
        float angle;

        if (direction == Vector2.up)
        {
            axis = Vector3.right;
            angle = 90f;
        }
        else if (direction == Vector2.down)
        {
            axis = Vector3.right;
            angle = -90f;
        }
        else if (direction == Vector2.right)
        {
            axis = Vector3.up;
            angle = -90f;
        }
        else // Vector2.left
        {
            axis = Vector3.up;
            angle = 90f;
        }

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, axis) * startRotation;

        float t = 0f;
        while (t < rotationDuration)
        {
            t += Time.deltaTime;
            float eased = rotationEase.Evaluate(Mathf.Clamp01(t / rotationDuration));
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, eased);
            yield return null;
        }

        transform.rotation = targetRotation; // guarantee exact final orientation
        isRotating = false;

        onSwipeCompleted?.Invoke(direction);
    }
}