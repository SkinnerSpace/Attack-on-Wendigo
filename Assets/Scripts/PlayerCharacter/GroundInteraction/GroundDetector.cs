using UnityEngine;

public class GroundDetector : MonoBehaviour, IGroundDetector
{
    public bool Check(Vector3 position, float radius) => Physics.CheckSphere(position, radius, ComplexLayers.Solid);
}
