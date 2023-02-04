using UnityEngine;
using System.Collections;

public class RagdollBone : MonoBehaviour
{
    public RagdollBoneStorage storage;

    private Rigidbody boneBody;
    private Collider boneCollider;
    private CharacterJoint boneJoint;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        InitializeComponents();
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
    }

    private void SetRigidBody(bool isRagdoll)
    {
        boneBody.velocity = Vector3.zero;
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
}
