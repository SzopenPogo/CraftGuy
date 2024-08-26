using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideCursor();
        LockCursor();

        UserInterfaceManager.Instance.OnFirstWindowOpen += EnableCursor;
        UserInterfaceManager.Instance.OnLastWindowClose += DisableCursor;
    }

    private void OnDestroy()
    {
        UserInterfaceManager.Instance.OnFirstWindowOpen -= EnableCursor;
        UserInterfaceManager.Instance.OnLastWindowClose -= DisableCursor;
    }

    private void EnableCursor()
    {
        ShowCursor();
        UnlockCursor();
    }

    private void DisableCursor()
    {
        HideCursor();
        LockCursor();
    }

    private void HideCursor()
    {
        if(!Cursor.visible)
            return;

        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        if (Cursor.visible)
            return;

        Cursor.visible = true;
    }

    private void LockCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            return;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        Cursor.lockState = CursorLockMode.None;
    }
}
