using UnityEngine;

public interface ISelectable
{
    public Transform RootTransform { get; }
    public void Select();
    public void Deselect();
}
