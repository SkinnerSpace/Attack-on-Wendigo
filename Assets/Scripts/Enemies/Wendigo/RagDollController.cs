﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RagDollController : MonoBehaviour
{
    private Animator animator;
    private RagdollBoneStorage storage;
    private SkeletonRestructurer restructurer;

    private bool ragdollIsEnabled;

    private void Awake() => InitializeComponents();

    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        storage = GetComponent<RagdollBoneStorage>();
        restructurer = GetComponent<SkeletonRestructurer>();
    }

    private void Start() => SwitchOff();

    public void TriggerRagdoll(Vector3 force, Vector3 hitPoint)
    {
        SwitchOn();
        ApplyForce(force, hitPoint);
    }

    private void ApplyForce(Vector3 force, Vector3 hitPoint)
    {
        RagdollBone closestBone = storage.ragdollBones.OrderBy(bone => Vector3.Distance(bone.transform.position, hitPoint)).First();
        closestBone.AddForceAtPosition(force, hitPoint);
    }

    public void SwitchOff() => EnableRagdoll(false);

    public void SwitchOn() => EnableRagdoll(true);

    private void EnableRagdoll(bool isRagdoll)
    {
        if (ragdollIsEnabled != isRagdoll){
            ragdollIsEnabled = true;

            DisableAnimator(isRagdoll);
            EnableBones(isRagdoll);
        }
    }

    private void DisableAnimator(bool isRagdoll) => animator.enabled = isRagdoll ? false : true;

    private void EnableBones(bool isRagdoll)
    {
        foreach (RagdollBone bone in storage.ragdollBones)
            bone.EnableRagdoll(isRagdoll);
    }

    private void EnablePullers()
    {
        foreach (RagdollPuller puller in storage.pullers)
            puller.Launch();
    }
}