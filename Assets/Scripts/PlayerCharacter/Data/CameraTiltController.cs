using UnityEngine;

public class CameraTiltController : IMovementController
{
    private ICharacterData data;
    private IChronos chronos;

    public CameraTiltController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Move(Vector3 inDirection)
    {
        Vector3 direction = new Vector3(0f, 0f, -inDirection.x);

        data.CameraTiltEuler += direction * chronos.DeltaTime;
        // Make it tilt till some threshold and then go back movement stops
    }
}

