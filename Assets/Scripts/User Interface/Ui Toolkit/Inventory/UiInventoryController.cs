using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UiInventoryController : UiWindow
{
    public event Action OnInventoryControllerDisable;

    public VisualElement Root { get; private set; }
    public Inventory AssignedInventory { get; private set; }

    [Header("Components")]
    [SerializeField] private UIDocument uiDocument;

    [field: Header("Templates")]
    [field: SerializeField] public VisualTreeAsset InventorySlotTemplate { get; private set; }

    [field: Header("Button Events")]
    [field: SerializeField] public UnityEvent OnCloseButtonClick { get; private set; }
    [field: SerializeField] public UnityEvent<InventoryItem> OnItemDropButtonClick { get; private set; }
    [field: SerializeField] public UnityEvent<InventoryItem> OnItemCraftButtonClick { get; private set; }


    private void OnEnable()
    {
        Root = uiDocument.rootVisualElement;

        AssignedInventory = PlayerInventory.Instance;

        UiInventoryCloseButton closeButton = new(this);
        UiInventorySlots inventorySlots = new(this);
    }

    private void OnDisable()
    {
        OnInventoryControllerDisable?.Invoke();
    }

    #region Dispatch Buttons Click
    public void DispatchCloseButtonClick()
    {
        if (!IsUnityEvent(OnCloseButtonClick, nameof(OnCloseButtonClick)))
            return;

        OnCloseButtonClick?.Invoke();
    }

    public void DispatchInventoryItemCraftInitialize(InventoryItem item)
    {
        if (!IsUnityEvent(OnItemCraftButtonClick, nameof(OnItemCraftButtonClick)))
            return;

        OnItemCraftButtonClick?.Invoke(item);
    }

    public void DispatchInventoryItemDropInitialize(InventoryItem item)
    {
        if (!IsUnityEvent(OnItemDropButtonClick, nameof(OnItemDropButtonClick)))
            return;

        OnItemDropButtonClick?.Invoke(item);
    }
    #endregion

    #region Tools
    private bool IsUnityEvent(UnityEventBase unityEvent, string eventName)
    {
        return UnityEventChecker.IsAnyUnityEvent(
            unityEvent, eventName, nameof(UiInventoryController));
    }
    #endregion
}
