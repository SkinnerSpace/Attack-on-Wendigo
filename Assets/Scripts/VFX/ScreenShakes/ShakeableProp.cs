using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeableProp : MonoBehaviour
{
    [SerializeField] private Transform positionAcceptor;
    [SerializeField] private Transform angleAcceptor;

    private Vector3 originalPos;
    private Vector3 originalAngle;

    private void Awake()
    {
        if (positionAcceptor != null) originalPos = positionAcceptor.position;
        if (angleAcceptor != null) originalAngle = angleAcceptor.eulerAngles;
    }

    public void Displace(ShakeDisplacement displacement)
    {
        if (positionAcceptor != null) positionAcceptor.position = originalPos + displacement.position;
        if (angleAcceptor != null) angleAcceptor.eulerAngles = originalAngle + displacement.angle;
    }
}
