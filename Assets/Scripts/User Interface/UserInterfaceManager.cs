using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private UiInventoryController inventoryController;

    private void Start()
    {
        InputReader.Instance.OnInventoryKeyDown += inventoryController.ToggleWindow;
    }

    private void OnDestroy()
    {
        InputReader.Instance.OnInventoryKeyDown -= inventoryController.ToggleWindow;
    }
}
