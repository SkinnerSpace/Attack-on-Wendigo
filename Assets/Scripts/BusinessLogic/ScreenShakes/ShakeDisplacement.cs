using UnityEngine;

public class ShakeDisplacement
{
    public Vector3 position { get; private set; } = Vector3.zero;
    public Vector3 angle { get; private set; } = Vector3.zero;

    public ShakeDisplacement() { }

    public ShakeDisplacement(Vector3 position, Vector3 angle)
    {
        this.position = position;
        this.angle = angle;
    }

    public void Accumulate(ShakeDisplacement displacement)
    {
        position += displacement.position;
        angle += displacement.angle;
    }

    public void Clear()
    {
        position = Vector3.zero;
        angle = Vector3.zero;
    }

    public override string ToString()
    {
        return "Position: " + position + " Angle: " + angle; 
    }
}

