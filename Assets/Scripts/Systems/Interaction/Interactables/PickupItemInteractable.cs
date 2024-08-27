using UnityEngine;

public class PickupItemInteractable : Interactable
{
    [field: SerializeField] public ItemData ItemData { get; private set; }

    private void Start()
    {
        Interaction.Initialize(transform);
    }
}
