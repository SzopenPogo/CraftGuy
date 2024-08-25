using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField, Range(MinDragValue, MaxDragValue)] private float drag;

    private Vector3 impact;
    private Vector3 dampingVelocity;
    private float verticalVelocity;

    private const float MinDragValue = 0f;
    private const float MaxDragValue = 10f;

    private void FixedUpdate()
    {
        HandleVertivalVelocity();

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public Vector3 GetMovement()
    {
        return impact + Vector3.up * verticalVelocity;
    }

    private void HandleVertivalVelocity()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
            return;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }
}
