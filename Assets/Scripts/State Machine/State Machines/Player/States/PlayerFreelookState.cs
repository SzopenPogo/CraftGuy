using UnityEngine;

public class PlayerFreelookState : PlayerBaseState
{
    private const string IdleAnimationName = "Idle";
    private const string FreelookBlendTreeName = "";

    private const float CrossFadeDuration = .25f;

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
        Debug.Log("Freelook State");
    }
}
