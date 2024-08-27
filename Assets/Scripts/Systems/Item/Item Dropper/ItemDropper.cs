using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform detectDropPositionRayOrigin;
    [SerializeField] private Transform rootTransform;

    [Header("Ray Settings")]
    [SerializeField] private LayerMask ignoredLayers;
    [SerializeField] private float raycastDistance;

    [SerializeField] private ItemData testItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DropItem(testItem);
            Debug.Log("DROP ITEM");
        }
    }

    private void OnDrawGizmos()
    {
        if (detectDropPositionRayOrigin == null)
            return;
        
        Gizmos.color = Color.yellow;

        Vector3 rayDirection = detectDropPositionRayOrigin.forward * raycastDistance;
        Gizmos.DrawRay(detectDropPositionRayOrigin.position, rayDirection);
    }

    public void DropItem(ItemData item)
    {
        GameObject droppedItem = Instantiate(item.ItemPickupPrefab);


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
