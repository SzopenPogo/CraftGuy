using System;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public event Action<InventoryItem> OnPrepareCrafting;
    public event Action OnCancelCrafting;

    [field: SerializeField] public Inventory Inventory { get; private set; }

    public void PrepareCrafting(InventoryItem mainRequiredItem)
    {
        OnPrepareCrafting?.Invoke(mainRequiredItem);
    }

    public void CancelCrafting()
    {
        OnCancelCrafting?.Invoke();
    }
}
