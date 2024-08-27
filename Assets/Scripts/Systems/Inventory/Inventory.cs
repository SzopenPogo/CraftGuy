using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChange;
    public event Action OnInventoryOpen;
    public event Action OnInventoryClose;

    public List<InventoryItem> Items { get; private set; } = new();

    protected bool IsInventoryOpen { get; private set; }

    public void OpenInventory()
    {
        if (IsInventoryOpen)
            return;

        OnInventoryOpen?.Invoke();

        IsInventoryOpen = true;
    }

    public void CloseInventory()
    {
        if(!IsInventoryOpen)
            return;

        OnInventoryClose?.Invoke();

        IsInventoryOpen = false;
    }

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
