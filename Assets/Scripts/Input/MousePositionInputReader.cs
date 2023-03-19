using UnityEngine;
using System;

public class MousePositionInputReader : InputReader
{
    private event Action<Vector2> onPosUpdate;

    private void Update() => ReadInputWhenActive();

    protected override void ReadInput() => onPosUpdate?.Invoke(input.MousePosition);

    public void Subscribe(IMousePosObserver observer) => onPosUpdate += observer.OnMousePosUpdate;
    public void Unsubscribe(IMousePosObserver observer) => onPosUpdate -= observer.OnMousePosUpdate;

}
