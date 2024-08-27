using UnityEngine;

public class PickupItemInteractable : Interactable
{
    [field: SerializeField] public ItemData ItemData { get; private set; }

    public void Pickup()
    {
        Destroy(gameObject);
    }
}
