using System;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    public event Action OnSelect;
    public event Action OnDeselect;

    [field: SerializeField] public Transform RootTransform { get; private set; }

    public void Select()
    {
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        OnDeselect?.Invoke();
    }
}
