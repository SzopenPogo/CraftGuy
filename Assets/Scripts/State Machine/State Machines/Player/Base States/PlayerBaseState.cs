using UnityEngine;

public abstract class PlayerBaseState : BaseState<PlayerStateMachine>
{
    protected PlayerBaseState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    private Vector2 smoothMovementInputVelocity;

    private const float MinMoveSpeed = 0f;

    #region Movement
    protected bool IsInputMoving()
    {
        return InputReader.Instance.MovementValue != Vector2.zero;
    }

    protected void Move(Vector3 motion, float speed, float deltaTime)
    {
        Vector3 motionWithForceAndSpeed = motion * speed + StateMachine.ForceReceiver.GetMovement();
        StateMachine.CharacterController.Move(motionWithForceAndSpeed * deltaTime);
    }

    protected void IdleMove(float deltaTime)
    {
        Move(Vector3.zero, MinMoveSpeed, deltaTime);
    }

    protected Vector2 GetMovementInput(Vector2 currentInput, float smoothValue)
    {
        return Vector2.SmoothDamp(currentInput,
            InputReader.Instance.MovementValue, ref smoothMovementInputVelocity, smoothValue);
    }

    protected Vector3 GetCameraMovement(Vector2 inputMovement)
    {
        Vector3 forward = StateMachine.MainCamera.transform.forward;
        Vector3 right = StateMachine.MainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * inputMovement.y +
            right * inputMovement.x;
    }

    protected void FaceMovementDirection(Vector3 movement, float rotationSpeed)
    {
        if (movement == Vector3.zero)
            return;

        StateMachine.CharacterController.transform.rotation = Quaternion.Lerp(
            StateMachine.RootTransform.rotation,
            Quaternion.LookRotation(movement),
            rotationSpeed * StateMachine.GetDeltaTime());
    }
    #endregion

    #region State Change
    public void OnFreelook()
    {
        StateMachine.SwitchState(new PlayerFreelookState(StateMachine));
    }

    public void OnStartInteraction(Interactable interactable)
    {
        StateMachine.SwitchState(new PlayerStartInteractionState(StateMachine, interactable));
    }

    public void OnPickup(PickupItemInteractable interactable)
    {
        StateMachine.SwitchState(new PlayerPickupState(StateMachine, interactable));
    }
    #endregion
}
