using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiInventorySlots
{
    private UiInventoryController inventoryController;

    private List<UiInventorySlot> renderedSlots = new();

    private VisualElement slotsContainer;

    public UiInventorySlots(UiInventoryController inventoryController)
    {
        this.inventoryController = inventoryController;

        slotsContainer = inventoryController.Root.Q<VisualElement>("Slots");

        RenderSlots();

        inventoryController.AssignedInventory.OnInventoryChange += RenderSlots;
        inventoryController.OnInventoryControllerDisable += HandleInventoryControllerDisable;
    }

    private void HandleInventoryControllerDisable()
    {
        inventoryController.AssignedInventory.OnInventoryChange -= RenderSlots;
        inventoryController.OnInventoryControllerDisable -= HandleInventoryControllerDisable;
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
}
