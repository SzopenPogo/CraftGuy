using UnityEngine;

public class PlayerFreelookState : PlayerBaseState
{
    private const string IdleAnimationName = "Idle";
    private const string FreelookBlendTreeName = "";

    private const float CrossFadeDuration = .25f;

    private Vector2 currentMovementInput;
    private Vector3 movement;

    public PlayerFreelookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        SetAnimation(IdleAnimationName, CrossFadeDuration);
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        float deltaTime = StateMachine.GetDeltaTime();

        if (!IsMoving())
            return;

        currentMovementInput = GetMovementInput(currentMovementInput, StateMachine.MovementValues.SmoothMovementInput);
        movement = GetCameraMovement(currentMovementInput);

        Move(StateMachine.RootTransform.forward, StateMachine.MovementValues.MovementSpeed, deltaTime);
        FaceMovementDirection(movement, StateMachine.MovementValues.RotationSpeed);
    }
}
