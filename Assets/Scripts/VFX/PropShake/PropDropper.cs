﻿using UnityEngine;

public class PropDropper : ICollapseObserver
{
    private const float DEPTH_OFFSET = 1f;

    public Vector3 posDisplacement { get; private set; }
    public Quaternion rotDisplacement { get; private set; }

    private Vector3 originalPos;
    private Quaternion originalRot;

    private Vector3 fallPosOffset;
    private Quaternion fallRotation;

    private float depthMultiplier;
    private float pushMultiplier;
    private float angleMultiplier;

    public PropDropper(CollapseAcceptor acceptor, float depthMultiplier, float pushMultiplier, float angleMultiplier)
    {
        originalPos = acceptor.originalPos;
        originalRot = acceptor.originalRot;

        float depth = -(acceptor.height + DEPTH_OFFSET) * 1.5f;
        fallPosOffset = new Vector3(0f, depth, 0f);

        this.depthMultiplier = depthMultiplier;
        this.pushMultiplier = pushMultiplier;
        this.angleMultiplier = angleMultiplier;
    }

    public void Launch(Vector3 pushDir)
    {
        fallPosOffset += GetDisplacementFromDirection(pushDir);
        fallRotation = CalculateFallRotation(pushDir, fallPosOffset);
    }

    private Vector3 GetDisplacementFromDirection(Vector3 direction) => new Vector3(direction.x, 0f, direction.z).normalized * pushMultiplier;

    private Quaternion CalculateFallRotation(Vector3 direction, Vector3 offset)
    {
        Vector3 fallPos = originalPos + offset;
        Vector3 fallDir = (fallPos - originalPos).normalized;
        fallDir = new Vector3(fallDir.x, fallDir.y * angleMultiplier, fallDir.z);

        float adjustmentAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        Quaternion adjustmentRotation = Quaternion.Euler(0f, -adjustmentAngle, 0f);

        return Quaternion.LookRotation(fallDir, Vector3.up) * originalRot * adjustmentRotation;
    }

    public void ReceiveCollapseUpdate(float displacement)
    {
        Vector3 fallPosAdjusted = new Vector3(fallPosOffset.x * 0.2f, fallPosOffset.y * depthMultiplier, fallPosOffset.z * 0.2f);
        posDisplacement = Vector3.Lerp(Vector3.zero, fallPosAdjusted, displacement);
        //posDisplacement = Vector3.Lerp(Vector3.zero, fallPosOffset, displacement);
        rotDisplacement = Quaternion.Slerp(originalRot, fallRotation, displacement);
    }
}

