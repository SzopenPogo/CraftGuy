using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public event Action<Interactable> OnInteractionInitialized;

    [SerializeField] private Selector selector;

    private void Start()
    {
        InputReader.Instance.OnInteractionKeyDown += TryInitializeInteraction;
    }

    private void OnDestroy()
    {
        InputReader.Instance.OnInteractionKeyDown -= TryInitializeInteraction;
    }

    private void TryInitializeInteraction()
    {
        if (!TryGetInteractableFromSelector(out Interactable interactable))
            return;

        OnInteractionInitialized?.Invoke(interactable);
    }

    private bool TryGetInteractableFromSelector(out Interactable interactable)
    {
        if (!selector.IsSomethingSelected())
        {
            interactable = null;
            return false;
        }

        return selector.SelectedSelectable.RootTransform.TryGetComponent(out interactable);
    }
}
