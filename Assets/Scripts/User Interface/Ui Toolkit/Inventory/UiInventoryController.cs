using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UiInventoryController : UiWindow
{
    public event Action OnInventoryControllerDisable;

    public VisualElement Root { get; private set; }

    [Header("Components")]
    [SerializeField] private UIDocument uiDocument;


    private void OnEnable()
    {
        Root = uiDocument.rootVisualElement;

        UiInventoryCloseButton closeButton = new(this);
    }

    private void OnDisable()
    {
        OnInventoryControllerDisable?.Invoke();
    }
}
