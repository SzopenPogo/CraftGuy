using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingRecipe
{
    private UiCraftingRecipes uiCraftingRecipes;
    private CraftingRecipe craftingRecipe;

    private VisualElement container;
    private VisualElement icon;
    private Label title;
    private Label description;
    private VisualElement components;

    private bool isSelected;

    private const string SelectedClassName = "recipe--selected";

    public UiCraftingRecipe(UiCraftingRecipes uiCraftingRecipes, VisualElement renderedRecipe,
        CraftingRecipe craftingRecipe)
    {

        this.uiCraftingRecipes = uiCraftingRecipes;
        this.craftingRecipe = craftingRecipe;

        container = renderedRecipe.Q<VisualElement>("Recipe");
        icon = renderedRecipe.Q<VisualElement>("Recipe-Icon");
        title = renderedRecipe.Q<Label>("Recipe-Title");
        description = renderedRecipe.Q<Label>("Recipe-Description");
        components = renderedRecipe.Q<VisualElement>("Recipe-Components");

        InitializeUiElements();
        RenderRecipeComponents();

        container.RegisterCallback<ClickEvent>(RegisterSelect);

        uiCraftingRecipes.UiCraftingController.OnWindowDisable += Deinitialize;
    }

    #region Initialization

    private void InitializeUiElements()
    {
        icon.style.backgroundImage = new StyleBackground(craftingRecipe.RecipeCraftItem.ItemIcon);
        title.text = craftingRecipe.RecipeCraftItem.GetItemName();
        description.text = craftingRecipe.GetDescription();
    }

    private void Deinitialize()
    {
        container.UnregisterCallback<ClickEvent>(RegisterSelect);
        uiCraftingRecipes.UiCraftingController.OnWindowDisable -= Deinitialize;
    }
    #endregion

    #region Render Recipe Components
    private void RenderRecipeComponents()
    {
        components.Clear();

        bool isMainRequiredItemRendered = false;

        foreach (ItemData itemData in craftingRecipe.RequiredItems)
        {
            if (!isMainRequiredItemRendered 
                && itemData == uiCraftingRecipes.UiCraftingController.MainRequiredItem.ItemData)
            {
                RenderRecipeComponent(uiCraftingRecipes.UiCraftingController.MainRequiredItem, true);
                isMainRequiredItemRendered = true;
                continue;
            }

            if (uiCraftingRecipes.UiCraftingController.AssignedCrafting.Inventory.TryGetInventoryItem(
                itemData, uiCraftingRecipes.UiCraftingController.MainRequiredItem, out InventoryItem inventoryItem))
            {
                RenderRecipeComponent(inventoryItem, true);
                continue;
            }

            RenderRecipeComponent(new(itemData), false);
        }
    }

    private void RenderRecipeComponent(InventoryItem inventoryItem, bool isInInventory)
    {
        VisualElement renderedRecipe = uiCraftingRecipes.UiCraftingController.RecipeComponentTemplate.Instantiate();
        components.Add(renderedRecipe);

        UiCraftingRecipeComponent component = new(uiCraftingRecipes.UiCraftingController, renderedRecipe, inventoryItem,
            isInInventory);
    }
    #endregion

    #region Selection
    public void RegisterSelect(ClickEvent e)
    {
        uiCraftingRecipes.SelectRecipe(this);
    }

    public void Select()
    {
        if (isSelected)
            return;

        container.AddToClassList(SelectedClassName);

        isSelected = true;
    }

    public void Deselect()
    {
        if (!isSelected)
            return;

        container.RemoveFromClassList(SelectedClassName);

        isSelected = false;
    }
    #endregion
}
