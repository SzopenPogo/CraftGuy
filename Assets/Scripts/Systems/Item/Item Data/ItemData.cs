using UnityEngine;

[CreateAssetMenu(
    fileName = ItemDataScriptableObjectVariables.ItemValuesFileName,
    menuName = ItemDataScriptableObjectVariables.ItemValuesMenuName)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [field: SerializeField] public Sprite ItemIcon { get; private set; }

    public string GetItemName()
    {
        return itemName;
    }
}
