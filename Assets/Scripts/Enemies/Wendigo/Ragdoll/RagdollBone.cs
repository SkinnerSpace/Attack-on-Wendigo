using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RagdollBone : MonoBehaviour
{
    private const float FORCE_MULTIPLIER = 20f;

    public RagdollBoneStorage storage;

    private Rigidbody boneBody;
    private Collider boneCollider;
    private CharacterJoint boneJoint;

    [SerializeField] private List<Limb> limbs;
    private int limbsDestroyed;

    private bool isDestroyed;

    private void Awake()
    {
        InitializeComponents();

        if (limbs != null && limbs.Count > 0){
            foreach (Limb limb in limbs){
                limb.SubscribeOnAmputation(SwitchTheLayerIfNecessary);
            }
        }

        //SubscribeOnLimbs();
    }

    public void InitializeComponents()
    {
        boneBody = GetComponent<Rigidbody>();
        boneCollider = GetComponent<Collider>();
        boneJoint = GetComponent<CharacterJoint>();
    }

    public void LinkTheStorage(RagdollBoneStorage storage) => this.storage = storage;

    public void SwitchOn()
    {
        if (!isDestroyed){
            EnableRagdoll(true);
        }
    }
    public void SwitchOff() => EnableRagdoll(false);

    private void Destroy()
    {
        isDestroyed = true;
        //EnableRagdoll(false);
        boneCollider.enabled = false;
        Debug.Log("Ragdoll " + transform.name);
    }

    public void EnableRagdoll(bool isRagdoll)
    {
        SetRigidBody(isRagdoll);
        boneCollider.enabled = isRagdoll;
        if (boneJoint != null) boneJoint.enableCollision = isRagdoll;
    }

    private void SetRigidBody(bool isRagdoll)
    {
        boneBody.isKinematic = !isRagdoll;
        boneBody.useGravity = isRagdoll;
    }

    public void SetDrag(float drag) => boneBody.drag = drag;
    public void SetAngularDrag(float angularDrag) => boneBody.angularDrag = angularDrag;
    public void SetMass(float mass) => boneBody.mass = mass;
    public void AddForceAtPosition(Vector3 force, Vector3 position)
    {
        boneBody.AddForceAtPosition(force * FORCE_MULTIPLIER, position, ForceMode.Impulse);
    }

    public void SetInterpolate(bool interpolate) => boneBody.interpolation = interpolate ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;

    public void SetDetection(bool continuous) => boneBody.collisionDetectionMode = continuous ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Discrete;

    public void SetProjectionDistance(float projectionDistance){
        if (boneJoint != null)
        {
            boneJoint.projectionDistance = projectionDistance;
        }
    }

    public void SetContactDistance(float contactDistance)
    {
        if (boneJoint != null)
        {
            SoftJointLimit jointLimit1 = boneJoint.swing1Limit;
            jointLimit1.contactDistance = contactDistance;
            boneJoint.swing1Limit = jointLimit1;

            SoftJointLimit jointLimit2 = boneJoint.swing2Limit;
            jointLimit2.contactDistance = contactDistance;
            boneJoint.swing2Limit = jointLimit2;

            SoftJointLimit highTwist = boneJoint.highTwistLimit;
            highTwist.contactDistance = contactDistance;
            boneJoint.highTwistLimit = highTwist;

            SoftJointLimit lowTwist = boneJoint.lowTwistLimit;
            lowTwist.contactDistance = contactDistance;
            boneJoint.lowTwistLimit = lowTwist;
        }
    }

    public void SetDamper(float damper)
    {
        if (boneJoint != null)
        {
            SoftJointLimitSpring twistLimit = boneJoint.twistLimitSpring;
            twistLimit.damper = damper;
            boneJoint.twistLimitSpring = twistLimit;

            SoftJointLimitSpring swingLimit = boneJoint.swingLimitSpring;
            swingLimit.damper = damper;
            boneJoint.swingLimitSpring = swingLimit;
        }
    }

    public void SetSpring(float spring)
    {
        if (boneJoint != null)
        {
            SoftJointLimitSpring twistLimit = boneJoint.twistLimitSpring;
            twistLimit.spring = spring;
            boneJoint.twistLimitSpring = twistLimit;

            SoftJointLimitSpring swingLimit = boneJoint.swingLimitSpring;
            swingLimit.spring = spring;
            boneJoint.swingLimitSpring = swingLimit;
        }
    }

    private void SwitchTheLayerIfNecessary()
    {
        limbsDestroyed += 1;

        if (limbsDestroyed >= limbs.Count){
            gameObject.layer = (int)Layers.RagDoll;
        }
    }
}
