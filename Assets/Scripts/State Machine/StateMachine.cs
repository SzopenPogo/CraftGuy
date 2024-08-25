using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [field: Header("Base Components")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Transform RootTransform { get; private set; }

    private State currentState;

    private void Update()
    {
        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    public void SwitchState(State newState)
    {
        if (newState == null)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public float GetDeltaTime() =>
        Time.deltaTime;
}
