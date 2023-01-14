using UnityEngine;

public struct ShakeDisplacement
{
    public Vector3 position;
    public Vector3 angle;

    public ShakeDisplacement(Vector3 position, Vector3 angle)
    {
        this.position = position;
        this.angle = angle;
    }
}

