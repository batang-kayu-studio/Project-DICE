using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Lets the player freely rotate this object 360 degrees by dragging with mouse or touch.
/// Attach this to the parent GameObject of your dice.
/// Uses the new Input System's Pointer class, which unifies mouse and single-touch
/// input under one API — same code works in Editor (mouse) and on mobile builds (touch).
/// </summary>
public class DiceDragRotate : MonoBehaviour
{
    [Tooltip("Higher = faster rotation per pixel dragged")]
    [SerializeField] private float rotationSpeed = 0.3f;

    [Tooltip("If true, dice keeps spinning briefly after release, like a flick")]
    [SerializeField] private bool useInertia = true;
    [SerializeField] private float inertiaDamping = 3f;

    private Vector2 previousPosition;
    private bool isDragging;
    private Vector2 currentAngularVelocity;

    void Update()
    {
        HandleDragInput();

        if (!isDragging && useInertia)
        {
            ApplyInertia();
        }
    }

    private void HandleDragInput()
    {
        Pointer pointer = Pointer.current;
        if (pointer == null) return; // no mouse/touch device active yet

        if (pointer.press.wasPressedThisFrame)
        {
            isDragging = true;
            previousPosition = pointer.position.ReadValue();
            currentAngularVelocity = Vector2.zero;
        }
        else if (pointer.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 currentPosition = pointer.position.ReadValue();
            Vector2 delta = currentPosition - previousPosition;

            float yaw = -delta.x * rotationSpeed;
            float pitch = delta.y * rotationSpeed;

            transform.Rotate(Vector3.up, yaw, Space.World);
            transform.Rotate(Vector3.right, pitch, Space.World);

            // store velocity for inertia once released
            currentAngularVelocity = new Vector2(yaw, pitch) / Mathf.Max(Time.deltaTime, 0.0001f);

            previousPosition = currentPosition;
        }
    }

    private void ApplyInertia()
    {
        if (currentAngularVelocity.sqrMagnitude < 0.01f) return;

        transform.Rotate(Vector3.up, -currentAngularVelocity.x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, currentAngularVelocity.y * Time.deltaTime, Space.World);

        currentAngularVelocity = Vector2.Lerp(currentAngularVelocity, Vector2.zero, inertiaDamping * Time.deltaTime);
    }
}