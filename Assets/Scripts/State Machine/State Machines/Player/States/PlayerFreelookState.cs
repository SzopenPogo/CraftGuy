using UnityEngine;

public class PlayerFreelookState : PlayerBaseState
{
    private const string FreelookBlendTreeName = "Freelook Blend Tree";

    private readonly int AnimatorMoveSpeedVariable = Animator.StringToHash("Move Speed");

    private const float CrossFadeDuration = .25f;

    private Vector2 currentMovementInput;
    private Vector3 movement;
    private float timeInNoMove;

    private const float MinAnimatorMoveSpeed = 0f;
    private const float MaxAnimatorMoveSpeed = 1f;
    private const float MinTimeInNoMove = 0f;
    private const float MaxTimeInNoMove = 1f;

    public PlayerFreelookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        InitializeEnterAnimation();
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        float deltaTime = StateMachine.GetDeltaTime();

        HandleTimeInNoMove(deltaTime);
        HandleAnimatorMoveSpeed(deltaTime);
        HandleMovement(deltaTime);
    }

    #region Movement
    private void InitializeEnterAnimation()
    {
        if (!IsAnimationPlaying(FreelookBlendTreeName))
            SetAnimation(FreelookBlendTreeName, CrossFadeDuration);

        SetAnimatorFloatInstant(AnimatorMoveSpeedVariable, MinAnimatorMoveSpeed, StateMachine.GetDeltaTime());
    }

    private void HandleTimeInNoMove(float deltaTime)
    {
        if (IsMoving())
        {
            SetTimeInNoMove(timeInNoMove -= deltaTime);
            return;
        }

        SetTimeInNoMove(timeInNoMove += deltaTime);
    }

    private void SetTimeInNoMove(float newValue)
    {
        if (newValue == MinTimeInNoMove || newValue == MaxTimeInNoMove)
            return;

        timeInNoMove = Mathf.Clamp(newValue, MinTimeInNoMove, MaxTimeInNoMove);
    }

    private void HandleAnimatorMoveSpeed(float deltaTime)
    {
        if(!IsMoving() && timeInNoMove < StateMachine.MovementValues.TimeInNoInputActionToStopMove)
            return;

        float animatorMoveSpeedTarget = IsMoving() ? MaxAnimatorMoveSpeed : MinAnimatorMoveSpeed;

        SetAnimatorFloat(AnimatorMoveSpeedVariable, animatorMoveSpeedTarget,
            StateMachine.MovementValues.AnimatorMoveSpeedBlendTime, deltaTime);
    }

    private void HandleMovement(float deltaTime)
    {
        if (!IsMoving() && timeInNoMove > StateMachine.MovementValues.TimeInNoInputActionToStopMove)
            return;

        currentMovementInput = GetMovementInput(currentMovementInput, StateMachine.MovementValues.SmoothMovementInput);
        movement = GetCameraMovement(currentMovementInput);

        Move(StateMachine.RootTransform.forward, GetMovementSpeed(), deltaTime);
        FaceMovementDirection(movement, GetRotationSpeed());
    }

    private float GetMovementSpeed()
    {
        return StateMachine.MovementValues.MovementSpeed * GetAnimatorFloat(AnimatorMoveSpeedVariable);
    }

    private float GetRotationSpeed()
    {
        return StateMachine.MovementValues.RotationSpeed * GetAnimatorFloat(AnimatorMoveSpeedVariable);
    }
    #endregion
}
