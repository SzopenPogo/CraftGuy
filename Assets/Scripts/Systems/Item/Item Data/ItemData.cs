using UnityEngine;

[CreateAssetMenu(
    fileName = ItemDataScriptableObjectVariables.ItemValuesFileName,
    menuName = ItemDataScriptableObjectVariables.ItemValuesMenuName)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;

    public string GetItemName()
    {
        return itemName;
    }
}
