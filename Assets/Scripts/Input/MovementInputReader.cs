using System;
using UnityEngine;

public class MovementInputReader : InputReader
{
    private event Action<Vector3> onDirectionUpdate;

    private void Update()
    {
        float moveRight = input.Hold(keys.MoveRight) ? 1f : 0f;
        float moveLeft = input.Hold(keys.MoveLeft) ? 1f : 0f;
        float x = moveRight - moveLeft;

        float moveForward = input.Hold(keys.MoveForward) ? 1f : 0f;
        float moveBackward = input.Hold(keys.MoveBackward) ? 1f : 0f;
        float z = moveForward - moveBackward;

        Vector3 direction = new Vector3(x, 0f, z).normalized;
        onDirectionUpdate?.Invoke(direction);
    }

    public void Subscribe(IMovementReaderObserver reader) => onDirectionUpdate += reader.Move;

    public void Unsubscribe(IMovementReaderObserver reader) => onDirectionUpdate += reader.Move;
}


