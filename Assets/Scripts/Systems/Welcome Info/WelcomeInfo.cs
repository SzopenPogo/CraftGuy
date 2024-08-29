using System;
using UnityEngine;

public class WelcomeInfo : MonoBehaviour
{
    public static WelcomeInfo Instance { get; private set; }

    public event Action OnInitialized;
    public event Action OnDeinitialized;

    private const float InvokeInitializeTime = .5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(Initialize), InvokeInitializeTime);
    }

    private void Initialize()
    {
        OnInitialized?.Invoke();
    }

    public void Deinitialize()
    {
        OnDeinitialized?.Invoke();
        gameObject.SetActive(false);
    }
}
