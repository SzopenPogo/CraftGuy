using UnityEngine;
using UnityEngine.UIElements;

public class UiToolkitScrollHandler : MonoBehaviour
{
    public static UiToolkitScrollHandler Instance { get; private set; }

    [field: Header("Values")]
    [SerializeField, Range(MinScrollIntensity, MaxScrollIntensity)] private float scrollIntensity;

    private const float MinScrollIntensity = 0f;
    private const float MaxScrollIntensity = 30000f;

    private void Awake()
    {
        Instance = this;
    }

    public void OnScroll(WheelEvent e, ScrollView scrollView)
    {
        scrollView.verticalScroller.value += e.delta.y * scrollIntensity;
    }
}
