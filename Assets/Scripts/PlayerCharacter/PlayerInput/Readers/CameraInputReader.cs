using UnityEngine;
using System;

public class CameraInputReader : InputReader
{
    private event Action<Vector2> onMotionUpdate;

    private void Update()
    {
        Vector2 motion = input.MousePos * keys.MouseSensitivity * keys.MouseInversion;
        onMotionUpdate?.Invoke(motion);
    }

    public void Subscribe(ICamera observer) => onMotionUpdate += observer.Rotate;
    public void Unsubscribe(ICamera observer) => onMotionUpdate -= observer.Rotate;
}
