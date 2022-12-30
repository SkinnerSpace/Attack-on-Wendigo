using UnityEngine;

public class CameraFov : MonoBehaviour, ICharacterDependee
{
    public const float NORMAL_FOV = 60f;
    public const float HOOKSHOT_VOF = 100f;

    private Character character;
    private float targetFov;
    private float fov;

    public void SetUp(Character character)
    {
        this.character = character;
        targetFov = character.mainCamera.fieldOfView;
        fov = targetFov;
    }

    private void Update()
    {
        float fovSpeed = 4f;
        fov = Mathf.Lerp(fov, targetFov, fovSpeed * Time.deltaTime);
        character.mainCamera.fieldOfView = fov;
    }

    public void SetCameraFov(float targetFov)
    {
        this.targetFov = targetFov;
    }
}
