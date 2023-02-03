using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RagDollController : MonoBehaviour
{
    private Animator animator;
    private SkeletonRestructurer restructurer;
    private RagdollBoneStorage storage;

    private bool ragdollIsEnabled;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        restructurer = GetComponent<SkeletonRestructurer>();
        storage = GetComponent<RagdollBoneStorage>();
    }

    private void Start()
    {
        SwitchOff();
    }

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

    public void SwitchOn()
    {
        EnableRagdoll(true);

        /*if (!restructurer.isDone)
            restructurer.Restructure();*/
    }

    private void EnableRagdoll(bool isRagdoll)
    {
        if (ragdollIsEnabled != isRagdoll){
            ragdollIsEnabled = true;

            animator.enabled = isRagdoll ? false : true;

            foreach (RagdollBone bone in storage.ragdollBones)
                bone.EnableRagdoll(isRagdoll);

            foreach (RagdollPuller puller in storage.pullers)
                puller.Launch();
        }
    }
}
