using UnityEngine.UIElements;

public class UiCraftingRecipeComponent
{
    private InventoryItem item;
    private bool isInInventory;

    private VisualElement wrapper;
    private VisualElement icon;
    private Label title;

    private const string InactiveClassName = "recipe-component--inactive";

    public UiCraftingRecipeComponent(VisualElement renderedComponent,
        InventoryItem item, bool isInInventory)
    {
        this.item = item;
        this.isInInventory = isInInventory;

        wrapper = renderedComponent.Q<VisualElement>("Recipe-Component");
        icon = renderedComponent.Q<VisualElement>("Recipe-Component-Icon");
        title = renderedComponent.Q<Label>("Recipe-Component-Name");

        InitializeUiElements();
    }

    private void InitializeUiElements()
    {
        icon.style.backgroundImage = new StyleBackground(item.ItemData.ItemIcon);
        title.text = item.ItemData.GetItemName();

        if (!isInInventory)
            wrapper.AddToClassList(InactiveClassName);
        else
            wrapper.RemoveFromClassList(InactiveClassName);
    }
}
