using UnityEngine;

[CreateAssetMenu(
    fileName = InteractionsScriptableObjectVariables.InteractionFileName,
    menuName = InteractionsScriptableObjectVariables.InteractionMenuName)]
public class Interaction : ScriptableObject
{
    [SerializeField] private string interactionTitle;
    [field: SerializeField] public InteractionType InteractionType { get; private set; }

    public string GetInteractionTitle() =>
        interactionTitle;
}
