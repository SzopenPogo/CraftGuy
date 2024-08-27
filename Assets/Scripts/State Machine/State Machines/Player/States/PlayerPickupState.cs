using UnityEngine;

public class PlayerPickupState : PlayerBaseState
{
    private readonly Interactable Interactable;
    private PickupItemController itemController;

    private bool isItemPickedUp;

    public PlayerPickupState(PlayerStateMachine stateMachine, Interactable interactable) : base(stateMachine)
    {
        Interactable = interactable;
    }

    public override void Enter()
    { 
        if(Interactable is not PickupItemController)
        {
            OnFreelook();
            return;
        }

        itemController = Interactable as PickupItemController;
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        if (isItemPickedUp)
        {
            OnFreelook();
            return;
        }

        StateMachine.Inventory.AddItem(itemController.ItemData);
        itemController.Interaction.TryInteract();

        isItemPickedUp = true;
    }
}
