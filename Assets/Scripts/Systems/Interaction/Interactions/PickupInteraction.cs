using UnityEngine;

[CreateAssetMenu(
    fileName = InteractionsScriptableObjectVariables.PickupInteractionFileName,
    menuName = InteractionsScriptableObjectVariables.PickupInteractionMenuName)]
public class PickupInteraction : Interaction
{
    public override bool TryInteract()
    {
        if (RootTransform == null)
            return false;

        Destroy(RootTransform.gameObject);

        return base.TryInteract();
    }
}
