using UnityEngine;
using System.Collections;

public class RagdollBone : MonoBehaviour
{
    public RagdollBoneStorage storage;

    private Rigidbody boneBody;
    private Collider boneCollider;
    private CharacterJoint boneJoint;

    private Vector3 frozenVelocity;

    public Vector3 Position => transform.position;

    private float freezing;

    private bool isUnfreezing;
    private float unfreezeTime = 2f;
    private float currentTime = 0f;

    private bool stuck;
    private float stuckTime = 1f;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Update()
    {
        /*if (isUnfreezing)
        {
            boneBody.velocity = boneBody.velocity * freezing;
            boneBody.angularVelocity = boneBody.angularVelocity * freezing;

            currentTime += Time.deltaTime;
            float lerp = Easing.QuadEaseOut(Mathf.InverseLerp(0f, unfreezeTime, currentTime));
            freezing = Mathf.Lerp(0f, 1f, lerp);

            if (freezing >= 1f)
            {
                isUnfreezing = false;
            }
        }*/
/*
        if (stuck)
        {
            if (currentTime >= stuckTime)
            {
                stuck = false;
                boneBody.constraints = RigidbodyConstraints.None;
            }
        }*/
    }

    public void InitializeComponents()
    {
        boneBody = GetComponent<Rigidbody>();
        boneCollider = GetComponent<Collider>();
        boneJoint = GetComponent<CharacterJoint>();
    }

    public void LinkTheStorage(RagdollBoneStorage storage) => this.storage = storage;

    public void SwitchOn() => EnableRagdoll(true);
    public void SwitchOff() => EnableRagdoll(false);

    public void EnableRagdoll(bool isRagdoll)
    {
        SetRigidBody(isRagdoll);
        boneCollider.enabled = isRagdoll;
        if (boneJoint != null) boneJoint.enableCollision = isRagdoll;

        if (isRagdoll)
        {
            //boneBody.constraints = RigidbodyConstraints.FreezeAll;
            //stuck = true;
            isUnfreezing = true;
        }
    }

    private void SetRigidBody(bool isRagdoll)
    {
/*        boneBody.velocity = Vector3.zero;
        boneBody.angularVelocity = Vector3.zero;*/
        boneBody.isKinematic = !isRagdoll;

        boneBody.useGravity = isRagdoll;
    }

    public void SetDrag(float drag) => boneBody.drag = drag;
    public void SetAngularDrag(float angularDrag) => boneBody.angularDrag = angularDrag;
    public void SetMass(float mass) => boneBody.mass = mass;
    public void AddForceAtPosition(Vector3 force, Vector3 position) => boneBody.AddForceAtPosition(force, position, ForceMode.Impulse);
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
}
