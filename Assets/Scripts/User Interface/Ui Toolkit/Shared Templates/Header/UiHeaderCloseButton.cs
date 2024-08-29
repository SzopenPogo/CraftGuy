using UnityEngine.UIElements;

public class UiHeaderCloseButton
{
    private UiToolkitWindow inventoryController;
    private Button closeButton;

    public UiHeaderCloseButton(UiToolkitWindow toolkitWindow)
    {
        this.inventoryController = toolkitWindow;

        closeButton = toolkitWindow.Root.Q<Button>("Header-Close-Button");
        closeButton.RegisterCallback<ClickEvent>(CloseButtonClick);

        toolkitWindow.OnWindowDisable += HandleInventoryDisable;
    }

    private void HandleInventoryDisable()
    {
        closeButton.UnregisterCallback<ClickEvent>(CloseButtonClick);

        inventoryController.OnWindowDisable -= HandleInventoryDisable;
    }

    private void CloseButtonClick(ClickEvent e)
    {
        inventoryController.DispatchCloseButtonClick();
    }
}
