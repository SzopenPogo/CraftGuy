using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public Interaction Interaction { get; private set; }
}
