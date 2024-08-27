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
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(PickupAnimationTag);

        if(!isItemPickedUp && normalizedTime >= StateMachine.PickupValues.PickupItemNormalizedTime)
        {
            if (PickupInteractable.Interaction.TryInteract())
                StateMachine.Inventory.AddItem(PickupInteractable.ItemData);

            isItemPickedUp = true;
        }

        if(normalizedTime >= StateMachine.PickupValues.ExitStateNormalizedTime)
        {
            OnFreelook();
            return;
        }
    }
}
