using UnityEngine;

public class ShakeDisplacement : IShakeDisplacement
{
    public Vector3 Position { get; private set; } 
    public Vector3 Angle { get; private set; } 

    public ShakeDisplacement() { }

    public ShakeDisplacement(Vector3 position, Vector3 angle)
    {
        Position = position;
        Angle = angle;
    }

    public void Accumulate(IShakeDisplacement displacement)
    {
        Position += displacement.Position;
        Angle += displacement.Angle;
    }

    public void Clear()
    {
        Position = Vector3.zero;
        Angle = Vector3.zero;
    }

    public bool IsNotEmpty() => Position.magnitude > Mathf.Epsilon || Angle.magnitude > Mathf.Epsilon;

    public override string ToString()
    {
        return "Position: " + Position + " Angle: " + Angle; 
    }
}

