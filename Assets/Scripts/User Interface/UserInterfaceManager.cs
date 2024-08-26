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
        InputReader.Instance.OnInventoryKeyDown += ToggleInventoryWindow;
    }

    private void OnDestroy()
    {
        InputReader.Instance.OnInventoryKeyDown -= ToggleInventoryWindow;
    }

    private void ToggleInventoryWindow()
    {
        ToggleWindow(inventoryController);
    }

    private void ToggleWindow(UiWindow window)
    {
        TryOpenWindow(window);

        window.ToggleWindow();
    }

    private bool TryOpenWindow(UiWindow window)
    {
        if (openWindows.Contains(window))
            return false;

        openWindows.Add(window);

        if (openWindows.Count == 1)
            OnFirstWindowOpen?.Invoke();

        window.OnWindowHide += CloseWindow;

        return true;
    }

    private void CloseWindow(UiWindow window)
    {
        if (!openWindows.Contains(window))
            return;

        openWindows.Remove(window);

        if (openWindows.Count <= 0)
            OnLastWindowClose?.Invoke();

        window.OnWindowHide -= CloseWindow;
    }
}
