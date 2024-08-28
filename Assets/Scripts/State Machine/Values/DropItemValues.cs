using UnityEngine;

[CreateAssetMenu(
    fileName = StateMachineScriptableObjectVariables.DropItemValuesFileName,
    menuName = StateMachineScriptableObjectVariables.DropItemValuesMenuName)]
public class DropItemValues : ScriptableObject
{
    [field: SerializeField, Range(MinNormalizedTime, MaxNormalizedTime)] public float DropItemNormalizedTime { get; private set; }
    [field: SerializeField, Range(MinNormalizedTime, MaxNormalizedTime)] public float ExitStateNormalizedTime { get; private set; }

    private const float MinNormalizedTime = 0f;
    private const float MaxNormalizedTime = 1f;
}
