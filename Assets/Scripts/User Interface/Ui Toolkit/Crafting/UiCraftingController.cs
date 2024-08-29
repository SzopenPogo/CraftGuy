using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class UiCraftingController : UiToolkitWindow
{
    public Crafting AssignedCrafting { get; private set; }
    public InventoryItem MainRequiredItem { get; private set; }

    [field: Header("Templates")]
    [field: SerializeField] public VisualTreeAsset RecipeTemplate { get; private set; }
    [field: SerializeField] public VisualTreeAsset RecipeComponentTemplate { get; private set; }
    [field: SerializeField] public VisualTreeAsset NoRecipesFoundTemplate { get; private set; }

    [field: Header("Button Events")]
    [field: SerializeField] public UnityEvent<CraftingRecipe, InventoryItem> OnCraftButtonClick { get; private set; }

    public UiCraftingContainer UiCraftingContainer { get; private set; }
    public UiCraftingRecipes UiCraftingRecipes { get; private set; }

    protected override void ApplyOnEnable()
    {
        base.ApplyOnEnable();

        AssignedCrafting = PlayerCrafting.Instance;

        UiHeaderCloseButton closeButton = new(this);
        
        AssignedCrafting.Inventory.OnInventoryChange += HandleInventoryChanged;
    }

    protected override void ApplyOnDisable()
    {
        base.ApplyOnDisable();

        AssignedCrafting.Inventory.OnInventoryChange -= HandleInventoryChanged;
    }

    public void Initialize(InventoryItem requiredItem)
    {
        MainRequiredItem = requiredItem;

        UiCraftingRecipes = new(this);
        UiCraftingContainer = new(this);
    }

    private void HandleInventoryChanged()
    {
        if (AssignedCrafting.Inventory.IsInventoryItemInInventory(MainRequiredItem))
            return;

        AssignedCrafting.CancelCrafting();
    }

    public void DispatchCraftButtonClick(CraftingRecipe craftingRecipe)
    {
        OnCraftButtonClick?.Invoke(craftingRecipe, MainRequiredItem);
    }
}
