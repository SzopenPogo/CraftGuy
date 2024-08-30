using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UiInventoryController : UiToolkitWindow
{
    [field: Header("Templates")]
    [field: SerializeField] public VisualTreeAsset InventorySlotTemplate { get; private set; }

    [field: Header("Assigned Systems")]
    [field: SerializeField] public Inventory AssignedInventory { get; private set; }
    [field: SerializeField] public ItemDropper AssignedItemDropper { get; private set; }

    [field: Header("Item Buttons Events")]
    [field: SerializeField] public UnityEvent<InventoryItem> OnItemDropButtonClick { get; private set; }
    [field: SerializeField] public UnityEvent<InventoryItem> OnItemCraftButtonClick { get; private set; }


    protected override void ApplyOnEnable()
    {
        base.ApplyOnEnable();

        UiHeaderCloseButton closeButton = new(this);
        UiInventorySlots inventorySlots = new(this);
    }

    #region Dispatch Buttons Click
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
