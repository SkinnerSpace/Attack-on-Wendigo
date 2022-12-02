using UnityEngine;

public class CameraFov : MonoBehaviour
{
    public const float NORMAL_FOV = 60f;
    public const float HOOKSHOT_VOF = 100f;

    private Camera playerCamera;
    private float targetFov;
    private float fov;

    private void Awake()
    {
        playerCamera = GetComponent<Camera>();
        targetFov = playerCamera.fieldOfView;
        fov = targetFov;
    }

    private void Update()
    {
        float fovSpeed = 4f;
        fov = Mathf.Lerp(fov, targetFov, fovSpeed * Time.deltaTime);
        playerCamera.fieldOfView = fov;
    }

    public void SetCameraFov(float targetFov)
    {
        this.targetFov = targetFov;
    }
}
