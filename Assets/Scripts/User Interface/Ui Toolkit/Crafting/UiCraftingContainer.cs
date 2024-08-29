using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingContainer
{
    private UiCraftingController uiCraftingController;
    private VisualElement craftContainer;

    private VisualElement itemIcon;
    private Label itemTitle;
    private Label craftingStatus;
    private Button craftButton;

    private bool isCraftButtonEnabled;
    private UiCraftingRecipe selectedRecipe;

    private const string CraftButtonInactiveClassName = "craft-button--inactive";

    public UiCraftingContainer(UiCraftingController uiCraftingController)
    {
        this.uiCraftingController = uiCraftingController;

        craftContainer = uiCraftingController.Root.Q<VisualElement>("Craft-Container");

        VisualElement craftItem = uiCraftingController.Root.Q<VisualElement>("Craft-Item");
        itemIcon = craftItem.Q<VisualElement>("Icon");
        itemTitle = craftItem.Q<Label>("Name");

        craftingStatus = uiCraftingController.Root.Q<Label>("Crafting-Status");
        craftButton = uiCraftingController.Root.Q<Button>("Craft-Button");

        craftButton.RegisterCallback<ClickEvent>(HandleCraftButtonClick);

        ResetContainer();

        uiCraftingController.OnWindowDisable += Deinitialize;
        uiCraftingController.UiCraftingRecipes.OnCraftingRecipeSelect += InitializeSelectedRecipe;
        uiCraftingController.UiCraftingRecipes.OnCraftingRecipeDeselect += ResetContainer;
    }

    #region Main Managment
    private void ResetContainer()
    {
        itemIcon.style.backgroundImage = new StyleBackground();
        itemTitle.text = "No recipe selected";

        DisableCraftingStatus();
        DisableCraftButton();
    }

    private void Deinitialize()
    {
        craftButton.UnregisterCallback<ClickEvent>(HandleCraftButtonClick);

        uiCraftingController.UiCraftingRecipes.OnCraftingRecipeSelect -= InitializeSelectedRecipe;
        uiCraftingController.OnWindowDisable -= Deinitialize;
        uiCraftingController.UiCraftingRecipes.OnCraftingRecipeDeselect -= ResetContainer;
    }
    #endregion

    #region Craft Button

    private void DisableCraftButton()
    {
        craftButton.AddToClassList(CraftButtonInactiveClassName);
        isCraftButtonEnabled = false;
    }

    private void EnableCraftButton()
    {
        craftButton.RemoveFromClassList(CraftButtonInactiveClassName);
        isCraftButtonEnabled = true;
    }

    private void HandleCraftButtonClick(ClickEvent e)
    {
        if (!isCraftButtonEnabled)
            return;

        if (selectedRecipe == null)
            return;

        uiCraftingController.DispatchCraftButtonClick(selectedRecipe.CraftingRecipe);
    }
    #endregion

    #region Crafting Status
    private void EnableCraftingStatus(string text)
    {
        craftingStatus.text = text;
        craftingStatus.visible = true;
    }

    private void DisableCraftingStatus()
    {
        craftingStatus.visible = false;
    }

    #endregion

    #region Recipe Select
    private void InitializeSelectedRecipe(UiCraftingRecipe selectedRecipe)
    {
        this.selectedRecipe = selectedRecipe;

        itemIcon.style.backgroundImage = new StyleBackground(selectedRecipe.CraftingRecipe.RecipeCraftItem.ItemIcon);
        itemTitle.text = selectedRecipe.CraftingRecipe.RecipeCraftItem.GetItemName();
        

        if (uiCraftingController.AssignedCrafting.IsSecondRequiredItem(
            selectedRecipe.CraftingRecipe, uiCraftingController.MainRequiredItem))
        {
            EnableCraftButton();
            EnableCraftingStatus("Ready to craft");
        }
        else
        {
            DisableCraftButton();
            EnableCraftingStatus("Not enough resources");
        }
    }

    #endregion
}
