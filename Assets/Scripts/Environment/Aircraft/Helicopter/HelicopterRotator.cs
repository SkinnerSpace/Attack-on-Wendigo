﻿using UnityEngine;

public class HelicopterRotator : MonoBehaviour, IHelicopterTimeObserver
{
    private float completion;

    public void UpdateCompletion(float completion) => this.completion = Easing.QuadEaseInOut(completion);

    public Quaternion Rotate(Quaternion prevRotation, Vector3 currentPos, Vector3 prevPos)
    {
        Quaternion fullRotation = GetFullRotation(currentPos, prevPos);
        Quaternion rotation = GetRotation(prevRotation, fullRotation);

        return (completion < 1f) ? rotation : prevRotation;
    }

    private Quaternion GetFullRotation(Vector3 currentPos, Vector3 prevPos)
    {
        Vector3 direction = (currentPos - prevPos).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion fullRotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

        return fullRotation;
    }

    private Quaternion GetRotation(Quaternion prevRotation, Quaternion fullRotation) => Quaternion.Slerp(prevRotation, fullRotation, completion);
}