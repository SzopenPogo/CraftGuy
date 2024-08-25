using UnityEngine;

public abstract class PlayerBaseState : BaseState<PlayerStateMachine>
{
    protected PlayerBaseState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    private const float MinMoveSpeed = 0f;

    #region Movement
    protected void Move(Vector3 motion, float speed, float deltaTime)
    {
        Vector3 motionWithForceAndSpeed = motion * speed + StateMachine.ForceReceiver.GetMovement();
        StateMachine.CharacterController.Move(motionWithForceAndSpeed * deltaTime);
    }

    protected void IdleMove(float deltaTime)
    {
        Move(Vector3.zero, MinMoveSpeed, deltaTime);
    }
    #endregion

    #region State Change
    public void OnFreelook()
    {
        StateMachine.SwitchState(new PlayerFreelookState(StateMachine));
    }
    #endregion
}
