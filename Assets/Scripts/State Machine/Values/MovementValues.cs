using UnityEngine;

[CreateAssetMenu(
    fileName = StateMachineScriptableObjectVariables.MovementValuesFileName, 
    menuName = StateMachineScriptableObjectVariables.MovementValuesMenuName)]
public class MovementValues : ScriptableObject
{
    [field: SerializeField, Range(MinMovementSpeed, MaxMovementSpeed)] public float MovementSpeed { get; private set; }
    [field: SerializeField, Range(MinRotationSpeed, MaxRotationSpeed)] public float RotationSpeed { get; private set; }
    [field: SerializeField, Range(MinSmoothMovementInputSpeed, MaxSmoothMovementInputSpeed)] public float SmoothMovementInput { get; private set; }

    private const float MinMovementSpeed = 0f;
    private const float MaxMovementSpeed = 10f;

    private const float MinRotationSpeed = 0f;
    private const float MaxRotationSpeed = 10f;

    private const float MinSmoothMovementInputSpeed = 0f;
    private const float MaxSmoothMovementInputSpeed = 10f;
}
