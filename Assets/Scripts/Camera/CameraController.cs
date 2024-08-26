using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freelookCamera;

    float defaultCameraXSpeed;
    float defaultCameraYSpeed;
    private bool isCameraLocked;

    private const float LockCameraSpeed = 0f;

    private void Start()
    {
        defaultCameraXSpeed = freelookCamera.m_XAxis.m_MaxSpeed;
        defaultCameraYSpeed = freelookCamera.m_YAxis.m_MaxSpeed;

        UserInterfaceManager.Instance.OnFirstWindowOpen += LockCamera;
        UserInterfaceManager.Instance.OnLastWindowClose += UnlockCamera;

    }

    private void OnDestroy()
    {
        UserInterfaceManager.Instance.OnFirstWindowOpen -= LockCamera;
        UserInterfaceManager.Instance.OnLastWindowClose -= UnlockCamera;
    }

    private void LockCamera()
    {
        if (isCameraLocked)
            return;

        freelookCamera.m_XAxis.m_MaxSpeed = LockCameraSpeed;
        freelookCamera.m_YAxis.m_MaxSpeed = LockCameraSpeed;

        isCameraLocked = true;
    }

    private void UnlockCamera()
    {
        if(!isCameraLocked) 
            return;

        freelookCamera.m_XAxis.m_MaxSpeed = defaultCameraXSpeed;
        freelookCamera.m_YAxis.m_MaxSpeed = defaultCameraYSpeed;

        isCameraLocked = false;
    }
}
