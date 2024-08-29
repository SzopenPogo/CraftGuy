using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public abstract class UiToolkitWindow : UiWindow
{
    public event Action OnWindowDisable;

    public VisualElement Root { get; private set; }

    [Header("Components")]
    [SerializeField] private UIDocument uiDocument;

    [field: Header("Window Button Events")]
    [field: SerializeField] public UnityEvent OnCloseButtonClick { get; private set; }

    private void OnEnable()
    {
        ApplyOnEnable();
    }

    private void OnDisable()
    {
        ApplyOnDisable();
    }

    protected virtual void ApplyOnEnable()
    {
        Root = uiDocument.rootVisualElement;
    }

    protected virtual void ApplyOnDisable()
    {
        OnWindowDisable?.Invoke();
    }

    #region Button Events
    public void DispatchCloseButtonClick()
    {
        if(!UnityEventChecker.IsAnyUnityEvent(
            OnCloseButtonClick, nameof(OnCloseButtonClick), nameof(UiToolkitWindow)))
        {
            return;
        }

        OnCloseButtonClick?.Invoke();
    }

    #endregion
}
