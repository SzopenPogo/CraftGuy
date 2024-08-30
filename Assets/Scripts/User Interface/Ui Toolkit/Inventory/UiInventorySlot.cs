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

    private const string DisabledButtonClassName = "slot-item-button-disable";

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

        inventoryController.OnWindowDisable += HandleItemControllerDisable;
    }

    private void HandleItemControllerDisable()
    {
        DeinitializeButtons();

        inventoryController.OnWindowDisable -= HandleItemControllerDisable;
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

    #region Buttons
    private void OnCraftButtonClick(ClickEvent e)
    {
        inventoryController.DispatchInventoryItemCraftInitialize(inventoryItem);
    }

    private void OnDropButtonClick(ClickEvent e)
    {
        if (!inventoryController.AssignedItemDropper.IsStartDropItemsEnable)
            return;

        inventoryController.DispatchInventoryItemDropInitialize(inventoryItem);
    }

    private void InitializeButtons()
    {
        craftButton.RegisterCallback<ClickEvent>(OnCraftButtonClick);
        dropButton.RegisterCallback<ClickEvent>(OnDropButtonClick);

        if (!inventoryController.AssignedItemDropper.IsStartDropItemsEnable)
            DisableDropButton();
        else
            EnableDropButton();

        inventoryController.AssignedItemDropper.OnStartDropItemEnabled += EnableDropButton;
        inventoryController.AssignedItemDropper.OnStartDropItemDisabled += DisableDropButton;
    }

    private void DeinitializeButtons()
    {
        craftButton.UnregisterCallback<ClickEvent>(OnCraftButtonClick);
        dropButton.UnregisterCallback<ClickEvent>(OnDropButtonClick);

        inventoryController.AssignedItemDropper.OnStartDropItemEnabled -= EnableDropButton;
        inventoryController.AssignedItemDropper.OnStartDropItemDisabled -= DisableDropButton;
    }

    private void EnableDropButton()
    {
        dropButton.RemoveFromClassList(DisabledButtonClassName);
    }

    private void DisableDropButton()
    {
        dropButton.AddToClassList(DisabledButtonClassName);
    }
    #endregion
}
