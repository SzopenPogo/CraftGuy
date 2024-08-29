using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = CraftingScriptableObjectVariables.CraftingRecipeFileName,
    menuName = CraftingScriptableObjectVariables.CraftingRecipeMenuName)]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField] public ItemData RecipeCraftItem {  get; set; }
    [field: SerializeField] public List<ItemData> RequiredItems { get; private set; } = new();
}
