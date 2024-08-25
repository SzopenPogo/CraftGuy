using UnityEngine;

public class PlayerSelector : Selector
{
    [Header("Components")]
    [SerializeField] private Camera mainCamera;

    [Header("Values")]
    [SerializeField] private Vector3 boxCastSize;
    [SerializeField] private float boxCastDistance;
    [SerializeField] private LayerMask ignoredLayersMasks;

    private RaycastHit hittedObject;

    private void FixedUpdate()
    {
        HandleSelectableSelection();
    }

    private void HandleSelectableSelection()
    {
        if (!IsSelectorActive)
            return;

        if (Physics.BoxCast(mainCamera.transform.position, boxCastSize, mainCamera.transform.forward, 
            out hittedObject, transform.rotation, boxCastDistance, ~ignoredLayersMasks))
        {
            HandleSelectDeselect();
            return;
        }

        Deselect();
    }

    private void HandleSelectDeselect()
    {
        Transform hittedObjectTransform = hittedObject.collider.transform;

        if (!hittedObjectTransform.TryGetComponent(out ISelectable hittedSelectable))
        {
            Deselect();
            return;
        }

        if(SelectedSelectable != hittedSelectable)
            Deselect();

        if (SelectedSelectable != null)
            return;

        Select(hittedSelectable);
    }
}
