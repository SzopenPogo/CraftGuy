using System.Collections.Generic;
using System.Text;
using UnityEngine;


[CreateAssetMenu(
    fileName = CraftingScriptableObjectVariables.CraftingRecipeFileName,
    menuName = CraftingScriptableObjectVariables.CraftingRecipeMenuName)]
public class CraftingRecipe : ScriptableObject
{
    [field: SerializeField] public ItemData RecipeCraftItem {  get; set; }
    [field: SerializeField] public List<ItemData> RequiredItems { get; private set; } = new();
    [field: SerializeField, Range(MinCraftChance, MaxCraftChance)] public float CraftChance { get; private set; }
    [field: SerializeField, Range(MinCraftTime, MaxCraftTime)] public float CraftTime { get; private set; }

    private const float MinCraftChance = 0.001f;
    private const float MaxCraftChance = 100f;
    private const float MinCraftTime = 0f;
    private const float MaxCraftTime = 30f;

    public string GetDescription()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.AppendLine($"Craft chance: {CraftChance:F2}%");

        return stringBuilder.ToString();
    }
}
