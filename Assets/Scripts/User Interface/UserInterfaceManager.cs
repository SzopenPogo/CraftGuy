using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance { get; private set; }

    public event Action OnFirstWindowOpen;
    public event Action OnLastWindowClose;

    [SerializeField] private UiInventoryController inventoryController;

    private List<UiWindow> openWindows = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeInventoryUi();
    }

    private void OnDestroy()
    {
        DeinitializeInventoryUi();
    }

    #region Open / Close window
    private bool TryOpenWindow(UiWindow window)
    {
        if (openWindows.Contains(window))
            return false;

        openWindows.Add(window);

        if (openWindows.Count == 1)
            OnFirstWindowOpen?.Invoke();

        window.ShowWindow();

        return true;
    }

    private bool TryCloseWindow(UiWindow window)
    {
        if (!openWindows.Contains(window))
            return false;

        openWindows.Remove(window);

        if (openWindows.Count <= 0)
            OnLastWindowClose?.Invoke();

        window.HideWindow();

        return true;
    }

    #endregion

    #region Inventory Ui
    private void InitializeInventoryUi()
    {
        PlayerInventory.Instance.OnInventoryOpen += ShowInventoryUi;
        PlayerInventory.Instance.OnInventoryClose += HideInventoryUi;
    }

    private void DeinitializeInventoryUi()
    {
        PlayerInventory.Instance.OnInventoryOpen -= ShowInventoryUi;
        PlayerInventory.Instance.OnInventoryClose -= HideInventoryUi;
    }

    private void ShowInventoryUi()
    {
        TryOpenWindow(inventoryController);
    }

    private void HideInventoryUi()
    {
        TryCloseWindow(inventoryController);
    }
    #endregion
}
