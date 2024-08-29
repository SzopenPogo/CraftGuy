using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crafting : MonoBehaviour
{
    public event Action<InventoryItem> OnPrepareCrafting;
    public event Action OnCancelCrafting;
    public event Action OnCraftingStart;
    public event Action<bool> OnCraftingFinish;

    [field: SerializeField] public Inventory Inventory { get; private set; }
    [SerializeField] private List<CraftingRecipe> craftingRecipes = new();

    [field: SerializeField] public UnityEvent<bool> OnCraftItem { get; private set; }

    private bool isCraftingPrepared;
    private Coroutine craftingProgressCoroutine;

    #region Crafting Status
    public void PrepareCrafting(InventoryItem mainRequiredItem)
    {
        if (isCraftingPrepared)
            CancelCrafting();

        OnPrepareCrafting?.Invoke(mainRequiredItem);

        isCraftingPrepared = true;
    }

    public void CancelCrafting()
    {
        OnCancelCrafting?.Invoke();

        isCraftingPrepared = false;
    }

    public void StartCraftItem(CraftingRecipe recipe, InventoryItem mainRequiredItem)
    {
        if (!TryGetSecondRequiredItem(recipe, mainRequiredItem, out InventoryItem secondRequiredItem))
            return;

        List<InventoryItem> requiredInventoryItems = new()
        {
            mainRequiredItem,
            secondRequiredItem
        };

        AttemptCrafting(recipe, requiredInventoryItems);

        OnCraftingStart?.Invoke();

        return;
    }

    private void FinishCraftItem(bool isCrafted)
    {
        OnCraftingFinish?.Invoke(isCrafted);
    }

    #endregion

    #region Tools
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

    public bool TryGetSecondRequiredItem(CraftingRecipe craftingRecipe, InventoryItem mainRequiredItem,
        out InventoryItem secondRequiredItem)
    {
        bool isMainRequiredItemFound = false;

        foreach (ItemData itemData in craftingRecipe.RequiredItems)
        {
            if (!isMainRequiredItemFound && itemData == mainRequiredItem.ItemData)
            {
                isMainRequiredItemFound = true;
                continue;
            }

            if (!Inventory.TryGetInventoryItem(itemData, mainRequiredItem, out InventoryItem inventoryItem))
                continue;

            secondRequiredItem = inventoryItem;
            return true;
        }

        secondRequiredItem = null;
        return false;
    }

    public bool IsSecondRequiredItem(CraftingRecipe craftingRecipe, InventoryItem mainRequiredItem)
    {
        return TryGetSecondRequiredItem(craftingRecipe, mainRequiredItem, out InventoryItem secondRequiredItem);
    }
    #endregion

    #region Crafting
    private bool IsCraftItemChance(CraftingRecipe recipe)
    {
        float randomValue = UnityEngine.Random.Range(recipe.GetMinCraftChace(), recipe.GetMaxCraftChance());

        if (randomValue < recipe.CraftChance)
            return true;

        return false;
    }

    private bool TryCraftItem(CraftingRecipe recipe, List<InventoryItem> requiredItems)
    {
        foreach (InventoryItem requiredItem in requiredItems)
        {
            if (!Inventory.IsInventoryItemInInventory(requiredItem))
                return false;

            Inventory.RemoveItem(requiredItem);
        }

        Inventory.AddItem(recipe.RecipeCraftItem);

        return true;
    }

    private bool AttemptCrafting(CraftingRecipe recipe, List<InventoryItem> requiredItems)
    {
        bool isCrafted = IsCraftItemChance(recipe);

        if (isCrafted)
            isCrafted = TryCraftItem(recipe, requiredItems);

        OnCraftItem?.Invoke(isCrafted);
        return isCrafted;
    }

    #region Crafting Coroutine
    private void StartCraftingProgressCoroutine()
    {
        if (craftingProgressCoroutine != null)
            return;

        craftingProgressCoroutine = StartCoroutine(CraftingProgressCoroutine());
    }

    private void StopCraftingProgressCoroutine()
    {
        if (craftingProgressCoroutine == null)
            return;

        StopCoroutine(craftingProgressCoroutine);
        craftingProgressCoroutine = null;
    }

    private IEnumerator CraftingProgressCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

        }
    }
    #endregion
    #endregion
}
