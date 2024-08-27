using UnityEngine;

[CreateAssetMenu(
    fileName = StateMachineScriptableObjectVariables.PickupValuesFileName,
    menuName = StateMachineScriptableObjectVariables.PickupValuesMenuName)]
public class PickupValues : ScriptableObject
{
    [field: SerializeField, Range(MinNormalizedTime, MaxNormalizedTime)] public float ExitStateNormalizedTime { get; private set; }
    [field: SerializeField, Range(MinNormalizedTime, MaxNormalizedTime)] public float PickupItemNormalizedTime { get; private set; }

    private const float MinNormalizedTime = 0f;
    private const float MaxNormalizedTime = 1f;
}
