using UnityEngine;

[CreateAssetMenu(
    fileName = ItemDataScriptableObjectVariables.ItemValuesFileName,
    menuName = ItemDataScriptableObjectVariables.ItemValuesMenuName)]
public class ItemData : ScriptableObject
{
    [field: Header("Base Data")]
    [SerializeField] private string itemName;
    [field: SerializeField] public Sprite ItemIcon { get; private set; }
    [field: SerializeField] public GameObject ItemPickupPrefab { get; private set; }

    public string GetItemName()
    {
        return itemName;
    }
}
