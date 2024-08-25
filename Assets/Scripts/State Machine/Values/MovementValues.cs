using UnityEngine;

[CreateAssetMenu(
    fileName = StateMachineScriptableObjectVariables.MovementValuesFileName, 
    menuName = StateMachineScriptableObjectVariables.MovementValuesMenuName)]
public class MovementValues : ScriptableObject
{
    [field: SerializeField, Range(MinMovementSpeed, MaxMovementSpeed)] 
    public float MovementSpeed { get; private set; }

    [field: SerializeField, Range(MinRotationSpeed, MaxRotationSpeed)] 
    public float RotationSpeed { get; private set; }

    [field: SerializeField, Range(MinSmoothMovementInputSpeed, MaxSmoothMovementInputSpeed)] 
    public float SmoothMovementInput { get; private set; }

    [field: SerializeField, Range(MinAnimatorMoveSpeedBlendTimeSpeed, MaxAnimatorMoveSpeedBlendTimeSpeed)] 
    public float AnimatorMoveSpeedBlendTime { get; private set; }

    [field: SerializeField, Range(MinTimeInNoInputActionToStopMove, MaxTimeInNoInputActionToStopMove)]
    public float TimeInNoInputActionToStopMove { get; private set; }

    #region Consts
    private const float MinMovementSpeed = 0f;
    private const float MaxMovementSpeed = 10f;

    private const float MinRotationSpeed = 0f;
    private const float MaxRotationSpeed = 10f;

    private const float MinSmoothMovementInputSpeed = 0f;
    private const float MaxSmoothMovementInputSpeed = 10f;

    private const float MinAnimatorMoveSpeedBlendTimeSpeed = 0f;
    private const float MaxAnimatorMoveSpeedBlendTimeSpeed = 2f;

    private const float MinTimeInNoInputActionToStopMove = 0f;
    private const float MaxTimeInNoInputActionToStopMove = 1f;
    #endregion
}
