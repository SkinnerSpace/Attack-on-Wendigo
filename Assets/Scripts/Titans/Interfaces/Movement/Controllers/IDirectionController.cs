using UnityEngine;

public interface IDirectionController
{
    void LookAt(Vector3 targetPosition, float speed);
}