using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RagDollController : MonoBehaviour
{
    private Animator animator;
    private RagdollBoneStorage storage;

    private bool ragdollIsEnabled;

    private void Awake() => InitializeComponents();

    private void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        storage = GetComponent<RagdollBoneStorage>();
    }

    private void Start() => SwitchOff();

    public void ApplyForce(Vector3 force, Vector3 hitPoint)
    {
        RagdollBone closestBone = storage.ragdollBones.OrderBy(bone => Vector3.Distance(bone.transform.position, hitPoint)).First();
        closestBone.AddForceAtPosition(force, hitPoint);
    }
    public void SwitchOn() => EnableRagdoll(true);

    public void SwitchOff() => EnableRagdoll(false);


    private void EnableRagdoll(bool isRagdoll)
    {
        if (ragdollIsEnabled != isRagdoll){
            ragdollIsEnabled = true;

            DisableAnimator(isRagdoll);
            EnableBones(isRagdoll);
            EnablePullers();
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
