using UnityEngine;

public class CameraTiltController : IMovementController
{
    private ICharacterData data;
    private IChronos chronos;
    private float tiltSpeed = 5f;
    private float tiltMaxAngle = 5f;

    public CameraTiltController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Move(Vector3 inDirection)
    {
        float tiltAngle = -1 * inDirection.x * tiltMaxAngle;
        data.CameraTiltEuler = Vector3.Lerp(data.CameraTiltEuler, new Vector3(0f, 0f, tiltAngle), tiltSpeed * chronos.DeltaTime);
    }
}

