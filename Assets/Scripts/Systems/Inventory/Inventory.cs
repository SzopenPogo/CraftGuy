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

    #region Managment

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
    #endregion

    #region Add Item
    public void AddItem(ItemData itemData)
    {
        AddItem(new InventoryItem(itemData));
    }

    public void AddItem(InventoryItem inventoryItem)
    {
        Items.Add(inventoryItem);
        OnInventoryChange?.Invoke();
    }
    #endregion

    #region Remove Item
    public void RemoveItem(InventoryItem item)
    {
        if (!Items.Contains(item))
            return;

        Items.Remove(item);
        OnInventoryChange?.Invoke();
    }
    #endregion

    #region Check Item
    public bool IsInventoryItemInInventory(InventoryItem item)
    {
        return Items.Contains(item);
    }
    #endregion

    #region Get Item
    public bool TryGetInventoryItem(ItemData itemData, InventoryItem ignoredItem, out InventoryItem inventoryItem)
    {
        inventoryItem = null;

        foreach (InventoryItem item in Items)
        {
            if(ignoredItem != null && item == ignoredItem)
                continue;

            if (item.ItemData != itemData)
                continue;

            inventoryItem = item;
            return true;
        }

        return false;
    }
    #endregion
}
