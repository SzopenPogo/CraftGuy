using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public static InputReader Instance;

    private Controls controls;

    #region Values
    public Vector2 MovementValue { get; private set; }
    public Vector2 MouseMoveValue { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        controls = new();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        MouseMoveValue = context.ReadValue<Vector2>();
    }
}
