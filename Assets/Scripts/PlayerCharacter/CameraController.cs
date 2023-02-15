using UnityEngine;
using System.Collections;

public class CameraController : ICamera
{
    private ICharacterData data;
    private IChronos chronos;
    
    private float xAngle;
    private float yAngle;

    public CameraController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Rotate(Vector2 motion)
    {
        xAngle += motion.x * chronos.DeltaTime;
        yAngle += motion.y * chronos.DeltaTime;
        yAngle = Mathf.Clamp(yAngle, -90f, 90f);

        UpdateCameraRotation();
        UpdateBodyRotation();
    }

    private void UpdateCameraRotation()
    {
        data.CameraRotation = Quaternion.Euler(new Vector3(
            x: yAngle, 
            y: xAngle, 
            z: data.CameraEuler.z));
    }

    private void UpdateBodyRotation() => data.Euler = new Vector3(0f, xAngle, 0f);
}
