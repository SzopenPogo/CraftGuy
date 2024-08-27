using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Unity Components")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Camera MainCamera { get; private set; }

    [field: Header("Physics")]
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Components")]
    [field: SerializeField] public InteractionManager InteractionManager { get; private set; }
    [field: SerializeField] public Inventory Inventory { get; private set; }

    [field: Header("Values")]
    [field: SerializeField] public MovementValues MovementValues { get; private set; }

    protected override void ApplyStart()
    {
        SwitchState(new PlayerFreelookState(this));
    }

    protected override bool CheckIsComponents()
    {
        if (!CheckIsComponent(MainCamera, nameof(MainCamera)))
            return false;

        return base.CheckIsComponents();
    }
}
