using UnityEngine;

public class PlayerCancelCraftState : PlayerBaseState
{
    private const string CancelCraftingAnimationName = "Cancel Crafting";
    private const string CancelCraftingAnimationTag = "Cancel Crafting";
    private const float CrossFadeDuration = .2f;

    private const float MaxNormalizedTime = 1f;

    public PlayerCancelCraftState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (IsApplyCraftingPrepared())
            return;

        SetAnimation(CancelCraftingAnimationName, CrossFadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        if (IsApplyCraftingPrepared())
            return;

        float normalizeTime = GetNormalizedTime(CancelCraftingAnimationTag);

        if (normalizeTime >= MaxNormalizedTime)
        {
            OnFreelook();
            return;
        }
    }

    private bool IsApplyCraftingPrepared()
    {
        if (!StateMachine.Crafting.IsCraftingPrepared)
            return false;

        OnPrepareCrafting(true);
        return true;
    }
}
