using System;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public event Action<InventoryItem> OnPrepareCrafting;
    public event Action OnCancelCrafting;

    [field: SerializeField] public Inventory Inventory { get; private set; }
    [SerializeField] private List<CraftingRecipe> craftingRecipes = new();

    private bool isCraftingPrepared;

    public void PrepareCrafting(InventoryItem mainRequiredItem)
    {
        if(isCraftingPrepared) 
            CancelCrafting();

        OnPrepareCrafting?.Invoke(mainRequiredItem);

        isCraftingPrepared = true;
    }

    public void CancelCrafting()
    {
        OnCancelCrafting?.Invoke();

        isCraftingPrepared = false;
    }

    public List<CraftingRecipe> GetMainRequiredItemRecipes(InventoryItem mainRequiredItem)
    {
        List<CraftingRecipe> matchingRecipes = new();

        foreach (CraftingRecipe recipe in craftingRecipes)
        {
            if (matchingRecipes.Contains(recipe))
                continue;

            foreach (ItemData itemData in recipe.RequiredItems)
            {
                if (itemData != mainRequiredItem.ItemData)
                    continue;

                matchingRecipes.Add(recipe);
                break;
            }
        }

        return matchingRecipes;
    }
}
