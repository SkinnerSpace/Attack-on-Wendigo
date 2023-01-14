using UnityEngine;

public class Shakeable : MonoBehaviour
{
    [SerializeField] private Transform positionAcceptor;
    [SerializeField] private Transform angleAcceptor;

    public void Displace(ShakeDisplacement displacement)
    {
        positionAcceptor.localPosition = displacement.position;
        angleAcceptor.localEulerAngles = displacement.angle;
    }
} 

