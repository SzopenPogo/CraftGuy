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
    }

    public void HideCursor()
    {
        if(!Cursor.visible)
            return;

        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        if (Cursor.visible)
            return;

        Cursor.visible = true;
    }

    public void LockCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            return;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
            return;

        Cursor.lockState = CursorLockMode.None;
    }
}
