using UnityEngine;

public class PlayerStartInteractionState : PlayerBaseState
{
    private readonly Interactable Interactable;

    public PlayerStartInteractionState(PlayerStateMachine stateMachine,
        Interactable interactable) : base(stateMachine)
    {
        Interactable = interactable;
    }

    public override void Enter()
    {
        SetInteractionState();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    private void SetInteractionState()
    {
        if (TryStartPickupInteraction())
            return;

        OnFreelook();
    }

    private bool TryStartPickupInteraction()
    {
        if (Interactable.Interaction.InteractionType != InteractionType.Pickup)
            return false;

        if (Interactable is not PickupItemInteractable)
            return false;

        OnPickup(Interactable as PickupItemInteractable);
        return true;
    }
}
