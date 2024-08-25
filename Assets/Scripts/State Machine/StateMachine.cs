using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [field: Header("Base Components")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Transform RootTransform { get; private set; }

    private State currentState;

    private void Start()
    {
        if (!CheckIsComponents())
            return;

        ApplyStart();
    }
    private void Update()
    {
        currentState?.Tick();
    }

    private void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    #region State Managment
    public void SwitchState(State newState)
    {
        if (newState == null)
            return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    protected abstract void ApplyStart();
    #endregion

    #region Tools
    public float GetDeltaTime() =>
        Time.deltaTime;
    #endregion

    #region Component Check
    protected bool CheckIsComponent(Object component, string componentName)
    {
        if (component != null)
            return true;

        Debug.LogError($"[{nameof(StateMachine)}] {RootTransform.gameObject.name}: {componentName} not assigned!");

        return false;
    }

    protected virtual bool CheckIsComponents()
    {
        return true;
    }
    #endregion
}
