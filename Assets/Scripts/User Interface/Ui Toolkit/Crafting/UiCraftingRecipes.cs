using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingRecipes
{
    public UiCraftingController UiCraftingController { get; private set; }
    private VisualElement recipesContainer;
    private ScrollView recipes;

    private UiCraftingRecipe selectedRecipe;

    public UiCraftingRecipes(UiCraftingController uiCraftingController)
    {
        this.UiCraftingController = uiCraftingController;
        recipesContainer = uiCraftingController.Root.Q<VisualElement>("Recipes-Container");
        recipes = recipesContainer.Q<ScrollView>("Recipes");

        recipes.RegisterCallback<WheelEvent>(EnableSlotsWindowScroll);

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

        if(selectedRecipe != null)
            selectedRecipe.Deselect();

        selectedRecipe = recipe;
        selectedRecipe.Select();
    }
}
