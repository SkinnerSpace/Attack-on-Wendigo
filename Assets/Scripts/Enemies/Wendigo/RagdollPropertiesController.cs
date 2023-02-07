﻿using UnityEngine;

[ExecuteAlways]
public class RagdollPropertiesController : MonoBehaviour
{
    [Range(0f, 1000)]
    public float drag = 0.05f;

    [Range(0f, 1000)]
    public float angularDrag;

    [Range(0f, 100f)]
    public float mass;

    [Range(0f, 10f)]
    public float projectionDistance = 1f;

    [SerializeField] private bool interpolate;

    [SerializeField] private bool continuousDetection;

    private RagdollBoneStorage storage;

    private void OnEnable()
    {
        storage = transform.GetComponent<RagdollBoneStorage>();

        if (storage.isReady)
        {
            SetParameters();
        }
    }

    private void SetParameters()
    {
        foreach (RagdollBone bone in storage.ragdollBones)
        {
            bone.InitializeComponents();

            bone.SetDrag(drag);
            bone.SetAngularDrag(angularDrag);
            bone.SetInterpolate(interpolate);
            bone.SetDetection(continuousDetection);
            bone.SetProjectionDistance(projectionDistance);
            //bone.SetMass(mass);
        }
    }
}