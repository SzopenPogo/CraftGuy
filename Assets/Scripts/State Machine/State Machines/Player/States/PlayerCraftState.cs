using UnityEngine;

public class PlayerCraftState : PlayerBaseState
{
    private const string CraftAnimationName = "Craft Idle";
    private const float CrossFadeDuration = .2f;

    public PlayerCraftState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        SetAnimation(CraftAnimationName, CrossFadeDuration);

        StateMachine.Crafting.OnCraftingFinish += HandleCraftFinished;
    }

    public override void Exit()
    {
        StateMachine.Crafting.OnCraftingFinish -= HandleCraftFinished;
    }

    public override void Tick()
    {
    }

    private void HandleCraftFinished(bool isCrafted)
    {
        if(!isCrafted && StateMachine.Crafting.IsCraftingPrepared)
        {
            OnPrepareCrafting(true);
            return;
        }

        OnCancelCrafting();
    }
}
