using UnityEngine;

public interface IAirDispatcher
{
    Vector3 GetTheLandingPosition(Vector3 position, float minHeight);
    Vector3 GetEscapePosition();
}