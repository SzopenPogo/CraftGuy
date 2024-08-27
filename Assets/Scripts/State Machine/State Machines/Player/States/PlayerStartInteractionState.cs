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
        SetInteractionState(Interactable.Interaction);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    private void SetInteractionState(Interaction interaction)
    {
        if(interaction.InteractionType == InteractionType.Pickup)
        {
            OnPickup(Interactable);
            return;
        }

        OnFreelook();
    }
}
