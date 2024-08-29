using UnityEngine.UIElements;

public class UiHeaderCloseButton
{
    private UiToolkitWindow toolkitWindow;
    private Button closeButton;

    public UiHeaderCloseButton(UiToolkitWindow toolkitWindow)
    {
        this.toolkitWindow = toolkitWindow;

        closeButton = toolkitWindow.Root.Q<Button>("Header-Close-Button");
        closeButton.RegisterCallback<ClickEvent>(CloseButtonClick);

        toolkitWindow.OnWindowDisable += HandleInventoryDisable;
    }

    private void HandleInventoryDisable()
    {
        closeButton.UnregisterCallback<ClickEvent>(CloseButtonClick);

        toolkitWindow.OnWindowDisable -= HandleInventoryDisable;
    }

    private void CloseButtonClick(ClickEvent e)
    {
        toolkitWindow.DispatchCloseButtonClick();
    }
}
