using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UiInventoryController : UiWindow
{
    public event Action OnInventoryControllerDisable;
    public event Action<InventoryItem> OnInventoryItemCraftInitialize;
    public event Action<InventoryItem> OnInventoryItemDropInitialize;

    public VisualElement Root { get; private set; }
    public Inventory AssignedInventory { get; private set; }

    [Header("Components")]
    [SerializeField] private UIDocument uiDocument;

    [field: Header("Templates")]
    [field: SerializeField] public VisualTreeAsset InventorySlotTemplate { get; private set; }


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

    public void DispatchInventoryItemCraftInitialize(InventoryItem item)
    {
        OnInventoryItemCraftInitialize?.Invoke(item);
        Debug.Log("Dispatch Craft");
    }

    public void DispatchInventoryItemDropInitialize(InventoryItem item)
    {
        OnInventoryItemDropInitialize?.Invoke(item);
        Debug.Log("Dispatch Drop");
    }
}
