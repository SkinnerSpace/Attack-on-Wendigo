using UnityEngine;
using System;

public class MouseMotionInputReader : InputReader
{
    private event Action<Vector2> onMotionUpdate;

    private void Update()
    {
        Vector2 motion = input.MouseMotion * keys.MouseSensitivity * keys.MouseInversion;
        onMotionUpdate?.Invoke(motion);
    }

    public void Subscribe(ICamera observer) => onMotionUpdate += observer.Rotate;
    public void Unsubscribe(ICamera observer) => onMotionUpdate -= observer.Rotate;
}
