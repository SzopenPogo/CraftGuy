using System;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    public event Action OnSelect;
    public event Action OnDeselect;

    public void Select()
    {
        Debug.Log("SELECT");
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        Debug.Log("DESELECT");
        OnDeselect?.Invoke();
    }
}
