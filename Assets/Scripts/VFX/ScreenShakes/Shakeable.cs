using UnityEngine;

public class Shakeable : MonoBehaviour
{
    [SerializeField] private Transform positionAcceptor;
    [SerializeField] private Transform angleAcceptor;

    public void Displace(ShakeDisplacement displacement)
    {
        if (positionAcceptor != null) positionAcceptor.localPosition = displacement.position;
        if (angleAcceptor != null) angleAcceptor.localEulerAngles = displacement.angle;
    }
} 

