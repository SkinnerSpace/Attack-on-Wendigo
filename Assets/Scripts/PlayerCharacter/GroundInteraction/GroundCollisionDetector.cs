using UnityEngine;

public class GroundCollisionDetector : IGroundDetector
{
    public bool Check(Vector3 position, float radius) => Physics.CheckSphere(position, radius, ComplexLayers.Solid);
}
