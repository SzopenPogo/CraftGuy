using UnityEngine;

public abstract class UiWindow : MonoBehaviour
{
    public bool IsShowWindow;

    [SerializeField] private GameObject windowRoot;

    public void ToggleWindow()
    {
        if (IsShowWindow)
        {
            HideWindow();
            return;
        }

        ShowWindow();
    }

    public virtual void ShowWindow()
    {
        windowRoot.SetActive(true);

        IsShowWindow = true;
    }

    public virtual void HideWindow()
    {
        windowRoot.SetActive(false);

        IsShowWindow = false;
    }
}
