using UnityEngine;

public class PlayerPrepareCraftingState : PlayerBaseState
{
    private const string PrepareCraftingAnimationName = "Prepare Crafting";
    private const string PrepareCraftingIdleAnimationName = "Prepare Crafting Idle";
    private const string PrepareCraftingAnimationTag = "Prepare Crafting";

    private const float CrossFadeDuration = .2f;
    private const float ShortCrossFadeDuration = .1f;

    private bool isIdleAnimation;

    private float MaxNormalizedTime = 1f;


    public PlayerPrepareCraftingState(PlayerStateMachine stateMachine, bool wasPrepared) : base(stateMachine)
    {
        isIdleAnimation = wasPrepared;
    }

    public override void Enter()
    {
        if (isIdleAnimation)
            SetAnimation(PrepareCraftingIdleAnimationName, CrossFadeDuration);
        else
            SetAnimation(PrepareCraftingAnimationName, CrossFadeDuration);

        StateMachine.Crafting.OnCancelCrafting += HandleCancelCraft;
        StateMachine.Crafting.OnCraftingStart += HandleCraftStart;
    }

    public override void Exit()
    {
        StateMachine.Crafting.OnCancelCrafting -= HandleCancelCraft;
        StateMachine.Crafting.OnCraftingStart -= HandleCraftStart;
    }

    public override void Tick()
    {
        if(isIdleAnimation && !IsAnimationPlaying(PrepareCraftingIdleAnimationName))
            SetAnimation(PrepareCraftingIdleAnimationName, ShortCrossFadeDuration);

        if (IsCancelCraftingPrepared())
            return;

        if (isIdleAnimation)
            return;

        float normalizedTime = GetNormalizedTime(PrepareCraftingAnimationTag);

        if(normalizedTime >= MaxNormalizedTime)
        {
            SetAnimation(PrepareCraftingIdleAnimationName, CrossFadeDuration);
            isIdleAnimation = true;
        }
    }

    private void HandleCancelCraft()
    {
        OnCancelCrafting();
    }

    private void HandleCraftStart()
    {
        OnStartCrafting();
    }

    private bool IsCancelCraftingPrepared()
    {
        if (StateMachine.Crafting.IsCraftingPrepared)
            return false;

        OnCancelCrafting();
        return true;
    }
}
