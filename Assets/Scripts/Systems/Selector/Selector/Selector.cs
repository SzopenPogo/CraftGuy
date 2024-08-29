using System;
using UnityEngine;

public abstract class Selector : MonoBehaviour
{
    public event Action<ISelectable> OnSelect;
    public event Action<ISelectable> OnDeselect;

    public ISelectable SelectedSelectable { get; private set; }
    public bool IsSelectorActive { get; private set; } = false;

    public bool IsSomethingSelected()
    {
        if (!IsSelectorActive)
            return false;

        if (SelectedSelectable == null)
            return false;

        return true;
    }

    public void EnableSelector()
    {
        IsSelectorActive = true;
    }

    public void DisableSelector()
    {
        IsSelectorActive = false;

        Deselect();
    }

    protected void Select(ISelectable selectable)
    {
        selectable.Select();
        SelectedSelectable = selectable;

        OnSelect?.Invoke(selectable);

    }

    protected void Deselect()
    {
        if (SelectedSelectable == null)
            return;

        SelectedSelectable?.Deselect();
        OnDeselect?.Invoke(SelectedSelectable);

        SelectedSelectable = null;
    }
}
