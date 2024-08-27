using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    [SerializeField] private string interactionTitle;
    [field: SerializeField] public InteractionType InteractionType { get; private set; }

    protected bool IsInitialized;
    protected Transform RootTransform;

    public virtual void Initialize(Transform rootTransform)
    {
        RootTransform = rootTransform;
        IsInitialized = true;
    }

    public string GetInteractionTitle() =>
        interactionTitle;

    public virtual bool TryInteract()
    {
        if (!IsInitialized)
            return false;

        return true;
    }
}
