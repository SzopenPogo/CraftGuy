using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupItemUi : MonoBehaviour
{
    [Header("Pickup Item Scripts")]
    [SerializeField] private PickupItemController itemController;
    [SerializeField] private Selectable itemSelectable;

    [Header("UI Components")]
    [SerializeField] private RectTransform uiContainer;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemInteractionText;

    private void Start()
    {
        itemSelectable.OnSelect += InitializeUi;
        itemSelectable.OnDeselect += HideUi;
    }

    private void OnDestroy()
    {
        itemSelectable.OnSelect -= InitializeUi;
        itemSelectable.OnDeselect -= HideUi;
    }

    private void InitializeUi()
    {
        ShowUi();

        itemNameText.text = itemController.ItemData.GetItemName();
        itemInteractionText.text = "TODO: Item Interaction";

        LayoutRebuilder.ForceRebuildLayoutImmediate(uiContainer);
    }

    private void ShowUi()
    {
        uiContainer.gameObject.SetActive(true);
    }

    private void HideUi()
    {
        uiContainer.gameObject.SetActive(false);
    }
}
