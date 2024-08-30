using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemDropper : MonoBehaviour
{
    public event Action<InventoryItem> OnDropIntemStarted;
    public event Action OnStartDropItemEnabled;
    public event Action OnStartDropItemDisabled;
    
    [Header("Transforms")]
    [SerializeField] private Transform detectDropPositionRayOrigin;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private Transform itemsContainer;

    [Header("Ray Settings")]
    [SerializeField] private LayerMask ignoredLayers;
    [SerializeField] private float raycastDistance;

    public bool IsStartDropItemsEnable { get; private set; }

    private void OnDrawGizmos()
    {
        if (detectDropPositionRayOrigin == null)
            return;
        
        Gizmos.color = Color.yellow;

        Vector3 rayDirection = detectDropPositionRayOrigin.forward * raycastDistance;
        Gizmos.DrawRay(detectDropPositionRayOrigin.position, rayDirection);
    }

    public void EnableStartDropItems()
    {
        if (IsStartDropItemsEnable)
            return;

        OnStartDropItemEnabled?.Invoke();
        IsStartDropItemsEnable = true;
    }

    public void DisableStartDropItems()
    {
        if (!IsStartDropItemsEnable)
            return;

        OnStartDropItemDisabled?.Invoke();
        IsStartDropItemsEnable = false;
    }

    public void StartDropItem(InventoryItem item)
    {
        if (!IsStartDropItemsEnable)
            return;

        OnDropIntemStarted?.Invoke(item);
    }

    public void DropItem(ItemData item)
    {
        GameObject droppedItem = Instantiate(item.ItemPickupPrefab, itemsContainer);


        if (Physics.Raycast(detectDropPositionRayOrigin.position, detectDropPositionRayOrigin.forward, 
            out RaycastHit hit, raycastDistance, ~ignoredLayers, QueryTriggerInteraction.Ignore))
        {
            droppedItem.transform.position = hit.point;

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            droppedItem.transform.rotation = targetRotation;
        }
        else
        {
            droppedItem.transform.position = rootTransform.position + rootTransform.forward;
            droppedItem.transform.rotation = Quaternion.Euler(0f, rootTransform.rotation.eulerAngles.y, 0f);
        }
    }
}
