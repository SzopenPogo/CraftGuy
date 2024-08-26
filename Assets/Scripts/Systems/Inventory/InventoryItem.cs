public class InventoryItem
{
    public ItemData ItemData { get; private set; }

    public InventoryItem(ItemData itemData)
    {
        ItemData = itemData;
    }
}
