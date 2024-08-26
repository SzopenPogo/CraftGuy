using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChange;

    public List<InventoryItem> Items { get; private set; } = new();

    public void AddItem(ItemData itemData)
    {
        AddItem(new InventoryItem(itemData));
    }

    public void AddItem(InventoryItem inventoryItem)
    {
        Items.Add(inventoryItem);
        OnInventoryChange?.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        if (!Items.Contains(item))
            return;

        Items.Remove(item);
        OnInventoryChange?.Invoke();
    }
}
