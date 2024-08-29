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

    private const string RecipesHiddenClassName = "recipes-hidden";

    public UiCraftingRecipes(UiCraftingController uiCraftingController)
    {
        UiCraftingController = uiCraftingController;
        recipesContainer = uiCraftingController.Root.Q<VisualElement>("Recipes-Container");
        recipes = recipesContainer.Q<ScrollView>("Recipes");

        recipes.RegisterCallback<WheelEvent>(EnableSlotsWindowScroll);

        InitailizeRecipes();

        UiCraftingController.OnWindowDisable += Deinitialize;

        UiCraftingController.AssignedCrafting.Inventory.OnInventoryChange += InitailizeRecipes;

        UiCraftingController.AssignedCrafting.OnCraftingStart += HideRenderedRecipes;
        UiCraftingController.AssignedCrafting.OnCraftingFinish += HandleCraftFinished;
    }

    private void Deinitialize()
    {
        UiCraftingController.AssignedCrafting.Inventory.OnInventoryChange -= InitailizeRecipes;

        UiCraftingController.OnWindowDisable -= Deinitialize;

        UiCraftingController.AssignedCrafting.OnCraftingStart -= HideRenderedRecipes;
        UiCraftingController.AssignedCrafting.OnCraftingFinish -= HandleCraftFinished;
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

        if(matchingRecipes.Count <= 0)
        {
            VisualElement renderedRecipe = UiCraftingController.NoRecipesFoundTemplate.Instantiate();
            recipes.Add(renderedRecipe);
            return;
        }

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

    private void ShowRenderedRecipes()
    {
        recipes.RemoveFromClassList(RecipesHiddenClassName);
    }

    private void HideRenderedRecipes()
    {
        recipes.AddToClassList(RecipesHiddenClassName);
    }

    private void HandleCraftFinished(bool isCrafted)
    {
        ShowRenderedRecipes();
    }
}
