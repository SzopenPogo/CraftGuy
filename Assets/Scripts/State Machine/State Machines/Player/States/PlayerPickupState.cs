using UnityEngine;

public class PlayerPickupState : PlayerBaseState
{
    private const string PickupAnimationName = "Pickup";
    private const string PickupAnimationTag = "Pickup";
    private const float CrossFadeDuration = .2f;

    private readonly PickupItemInteractable PickupInteractable;

    private bool isItemPickedUp;

    public PlayerPickupState(PlayerStateMachine stateMachine, PickupItemInteractable interactable) : base(stateMachine)
    {
        PickupInteractable = interactable;
    }

    public override void Enter()
    {
        SetAnimation(PickupAnimationName, CrossFadeDuration);
        StateMachine.Selector.DisableSelector();
    }

    public override void Exit()
    {
        StateMachine.Selector.EnableSelector();
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(PickupAnimationTag);

        if(!isItemPickedUp && normalizedTime >= StateMachine.PickupValues.PickupItemNormalizedTime)
        {

            StateMachine.Inventory.AddItem(PickupInteractable.ItemData);
            PickupInteractable.Pickup();

            isItemPickedUp = true;
        }

        if(normalizedTime >= StateMachine.PickupValues.ExitStateNormalizedTime)
        {
            OnFreelook();
            return;
        }
    }
}
