using UnityEngine;

public class Shakeable : MonoBehaviour
{
    [SerializeField] private Transform positionAcceptor;
    [SerializeField] private Transform angleAcceptor;

    public void Displace(ShakeDisplacement displacement)
    {
        if (positionAcceptor != null) positionAcceptor.localPosition = displacement.position;
        if (angleAcceptor != null) angleAcceptor.localEulerAngles = GetModifiedAngle(displacement.angle.z);
    }

    private Vector3 GetModifiedAngle(float zAngle)
    {
        return new Vector3(angleAcceptor.localEulerAngles.x, 
                           angleAcceptor.localEulerAngles.y, 
                           zAngle);
    }
} 

