using System.Collections;
using UnityEngine;

public class WorldUiLookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private float refreshUiTime;

    private Coroutine refreshUiCoroutine;

    private void OnEnable()
    {
        mainCamera = Camera.main;

        LookAtCamera();
        StartRefreshUiCoroutine();
    }

    private void OnDisable()
    {
        StopRefreshUiCoroutine();
    }

    private void StartRefreshUiCoroutine()
    {
        if (refreshUiCoroutine != null)
            return;

        refreshUiCoroutine = StartCoroutine(RefreshUiCoroutine());
    }

    private void StopRefreshUiCoroutine()
    {
        if (refreshUiCoroutine == null)
            return;

        StopCoroutine(refreshUiCoroutine);
        refreshUiCoroutine = null;
    }

    private IEnumerator RefreshUiCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshUiTime);

            LookAtCamera();
        }
    }

    private void LookAtCamera()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
