using UnityEngine.UIElements;

public class UiInventoryCloseButton
{
    private UiInventoryController inventoryController;
    private Button closeButton;

    public UiInventoryCloseButton(UiInventoryController inventoryController)
    {
        this.inventoryController = inventoryController;

        closeButton = inventoryController.Root.Q<Button>("Header-Close-Button");
        closeButton.RegisterCallback<ClickEvent>(CloseButtonClick);

        inventoryController.OnInventoryControllerDisable += HandleInventoryDisable;
    }

    private void HandleInventoryDisable()
    {
        closeButton.UnregisterCallback<ClickEvent>(CloseButtonClick);

        inventoryController.OnInventoryControllerDisable -= HandleInventoryDisable;
    }

    private void CloseButtonClick(ClickEvent e)
    {
        inventoryController.DispatchCloseButtonClick();
    }
}
