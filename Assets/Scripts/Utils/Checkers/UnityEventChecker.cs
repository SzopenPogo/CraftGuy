using UnityEngine;
using UnityEngine.Events;

public static class UnityEventChecker
{
    public static bool IsAnyUnityEvent(UnityEventBase unityEvent, string eventName, string className)
    {
        if (unityEvent == null)
        {
            Debug.LogWarning($"{className} - {eventName}: UnityEvent is null!");
            return false;
        }

        if (unityEvent.GetPersistentEventCount() == 0)
        {
            Debug.LogWarning($"{className} - {eventName}: No action assigned!");
            return false;
        }

        return true;
    }
}
