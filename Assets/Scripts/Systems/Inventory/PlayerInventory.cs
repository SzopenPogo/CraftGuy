using UnityEngine;

public class PlayerInventory : Inventory
{
    [SerializeField] private ItemData testItem;

    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddItem(testItem);
            Debug.Log("ITEM ADDED");
        }
    }
}
