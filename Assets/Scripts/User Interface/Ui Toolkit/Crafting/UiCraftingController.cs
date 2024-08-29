using UnityEngine;
using UnityEngine.UIElements;

public class UiCraftingController : UiToolkitWindow
{
    public Crafting AssignedCrafting { get; private set; }

    public InventoryItem MainRequiredItem { get; private set; }

    [field: Header("Templates")]
    [field: SerializeField] public VisualTreeAsset RecipeTemplate { get; private set; }
    [field: SerializeField] public VisualTreeAsset RecipeComponentTemplate { get; private set; }

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

    public void SetMainRequiredItem(InventoryItem requiredItem)
    {
        MainRequiredItem = requiredItem;
    }

    private void HandleInventoryChanged()
    {
        if (AssignedCrafting.Inventory.IsInventoryItemInInventory(MainRequiredItem))
            return;

        AssignedCrafting.CancelCrafting();
    }
}
