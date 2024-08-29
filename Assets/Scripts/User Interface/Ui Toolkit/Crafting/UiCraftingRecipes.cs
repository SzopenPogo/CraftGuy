using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class UiCraftingRecipes
{
    public event Action<UiCraftingRecipe> OnCraftingRecipeSelect;
    public event Action OnCraftingRecipeDeselect;

    public UiCraftingController UiCraftingController { get; private set; }
    private VisualElement recipesContainer;
    private ScrollView recipes;

    private UiCraftingRecipe selectedRecipe;

    public UiCraftingRecipes(UiCraftingController uiCraftingController)
    {
        UiCraftingController = uiCraftingController;
        recipesContainer = uiCraftingController.Root.Q<VisualElement>("Recipes-Container");
        recipes = recipesContainer.Q<ScrollView>("Recipes");

        recipes.RegisterCallback<WheelEvent>(EnableSlotsWindowScroll);

        InitailizeRecipes();

        UiCraftingController.OnWindowDisable += Deinitialize;
        UiCraftingController.AssignedCrafting.Inventory.OnInventoryChange += InitailizeRecipes;
    }

    private void Deinitialize()
    {
        UiCraftingController.AssignedCrafting.Inventory.OnInventoryChange -= InitailizeRecipes;
        UiCraftingController.OnWindowDisable -= Deinitialize;
    }

    private void InitailizeRecipes()
    {
        DeselectRecipe();
        RenderRecipes();
    }

    private void RenderRecipes()
    {
        recipes.Clear();

        List<CraftingRecipe> matchingRecipes = UiCraftingController.AssignedCrafting.GetMainRequiredItemRecipes(
            UiCraftingController.MainRequiredItem);

        foreach (CraftingRecipe craftingRecipe in matchingRecipes)
        {
            VisualElement renderedRecipe = UiCraftingController.RecipeTemplate.Instantiate();
            recipes.Add(renderedRecipe);

            UiCraftingRecipe recipeUi = new(this, renderedRecipe, craftingRecipe);
        }
    }

    private void EnableSlotsWindowScroll(WheelEvent e)
    {
        UiToolkitScrollHandler.Instance.OnScroll(e, recipes);
    }

    public void SelectRecipe(UiCraftingRecipe recipe)
    {
        if (selectedRecipe == recipe)
            return;

        if (selectedRecipe != null)
            DeselectRecipe();

        selectedRecipe = recipe;
        selectedRecipe.Select();

        OnCraftingRecipeSelect?.Invoke(selectedRecipe);
    }

    private void DeselectRecipe()
    {
        if (selectedRecipe == null)
            return;

        selectedRecipe.Deselect();

        OnCraftingRecipeDeselect?.Invoke();
    }
}
