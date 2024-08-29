using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingRecipeComponent
{

    private UiCraftingController uiCraftingController;
    private VisualElement renderedComponent;
    private InventoryItem item;
    private bool isInInventory;

    private VisualElement wrapper;
    private VisualElement icon;
    private Label title;

    private const string InactiveClassName = "recipe-component--inactive";

    public UiCraftingRecipeComponent(UiCraftingController uiCraftingController, VisualElement renderedComponent,
        InventoryItem item, bool isInInventory)
    {
        this.uiCraftingController = uiCraftingController;
        this.renderedComponent = renderedComponent;
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
