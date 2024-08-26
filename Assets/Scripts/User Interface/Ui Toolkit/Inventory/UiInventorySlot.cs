using UnityEngine;
using UnityEngine.UIElements;

public class UiInventorySlot
{
    private UiInventoryController inventoryController;
    private InventoryItem inventoryItem;

    private VisualElement renderedSlot;
    private VisualElement slotIcon;
    private Label slotTitle;
    private Button craftButton;
    private Button dropButton;

    public UiInventorySlot(UiInventoryController inventoryController, VisualElement slotsContainer, 
        InventoryItem inventoryItem)
    {
        this.inventoryController = inventoryController;
        this.inventoryItem = inventoryItem;

        RenderSlot(slotsContainer);

        slotIcon = renderedSlot.Q<VisualElement>("Slot-Item-Data__Icon");
        slotTitle = renderedSlot.Q<Label>("Slot-Item-Data__Title");
        craftButton = renderedSlot.Q<Button>("Slot-Item-Buttons__Craft");
        dropButton = renderedSlot.Q<Button>("Slot-Item-Button__Drop");

        InitializeData();
        InitializeButtons();

        inventoryController.OnInventoryControllerDisable += HandleItemControllerDisable;
    }

    private void HandleItemControllerDisable()
    {
        DeinitializeButtons();

        inventoryController.OnInventoryControllerDisable -= HandleItemControllerDisable;
    }

    private void RenderSlot(VisualElement slotsContainer)
    {
        renderedSlot = inventoryController.InventorySlotTemplate.Instantiate();
        slotsContainer.Add(renderedSlot);
    }

    private void InitializeData()
    {
        slotIcon.style.backgroundImage = new(inventoryItem.ItemData.ItemIcon);
        slotTitle.text = inventoryItem.ItemData.GetItemName();
    }

    private void OnCraftButtonClick(ClickEvent e)
    {
        inventoryController.DispatchInventoryItemCraftInitialize(inventoryItem);
    }

    private void OnDropButtonClick(ClickEvent e)
    {
        inventoryController.DispatchInventoryItemDropInitialize(inventoryItem);
    }

    private void InitializeButtons()
    {
        craftButton.RegisterCallback<ClickEvent>(OnCraftButtonClick);
        dropButton.RegisterCallback<ClickEvent>(OnDropButtonClick);
    }

    private void DeinitializeButtons()
    {
        craftButton.UnregisterCallback<ClickEvent>(OnCraftButtonClick);
        dropButton.UnregisterCallback<ClickEvent>(OnDropButtonClick);
    }
}
