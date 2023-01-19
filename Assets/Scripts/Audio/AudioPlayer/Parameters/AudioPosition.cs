using UnityEngine;

public class AudioPosition : AudioParam
{
    private Vector3 position;

    public AudioPosition(Vector3 position)
    {
        this.position = position;
    }

    public override void ApplyTo(AudioEvent audioEvent)
    {
        audioEvent.Set3DPosition(position);
    }
}
