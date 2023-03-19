using UnityEngine;
using System;

public class MouseMotionInputReader : InputReader
{
    private event Action<Vector2> onMotionUpdate;

    private void Update() => ReadInputWhenActive();

    protected override void ReadInput(){
        Vector2 motion = input.MouseMotion * keys.MouseSensitivity * keys.MouseInversion;
        onMotionUpdate?.Invoke(motion);
    }

    public void Subscribe(IMouseMotionObserver observer) => onMotionUpdate += observer.ReceiveMotion;
    public void Unsubscribe(IMouseMotionObserver observer) => onMotionUpdate -= observer.ReceiveMotion;
}
