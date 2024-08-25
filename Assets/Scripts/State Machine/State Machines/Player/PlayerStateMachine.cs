using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Unity Components")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: Header("Physics")]
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreelookState(this));
    }
}
