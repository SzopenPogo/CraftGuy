using System.Collections.Generic;
using UnityEngine.UIElements;

public class UiInventorySlots
{
    private UiInventoryController inventoryController;

    private List<UiInventorySlot> renderedSlots = new();

    private ScrollView slotsContainer;

    public UiInventorySlots(UiInventoryController inventoryController)
    {
        this.inventoryController = inventoryController;

        slotsContainer = inventoryController.Root.Q<ScrollView>("Slots");

        RenderSlots();

        slotsContainer.RegisterCallback<WheelEvent>(EnableSlotsWindowScroll);

        inventoryController.AssignedInventory.OnInventoryChange += RenderSlots;
        inventoryController.OnWindowDisable += HandleInventoryControllerDisable;
    }

    private void HandleInventoryControllerDisable()
    {
        slotsContainer.UnregisterCallback<WheelEvent>(EnableSlotsWindowScroll);

        inventoryController.AssignedInventory.OnInventoryChange -= RenderSlots;
        inventoryController.OnWindowDisable -= HandleInventoryControllerDisable;
    }

    private void RenderSlots()
    {
        slotsContainer.Clear();

        foreach (InventoryItem item in inventoryController.AssignedInventory.Items)
        {
            UiInventorySlot slot = new(inventoryController, slotsContainer, item);
            renderedSlots.Add(slot);
        }
    }

    private void EnableSlotsWindowScroll(WheelEvent e)
    {
        UiToolkitScrollHandler.Instance.OnScroll(e, slotsContainer);
    }
}
