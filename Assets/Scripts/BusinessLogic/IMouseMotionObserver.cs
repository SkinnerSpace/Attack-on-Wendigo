using UnityEngine;

public interface IMouseMotionObserver
{
    void ReceiveMotion(Vector2 motion);
}