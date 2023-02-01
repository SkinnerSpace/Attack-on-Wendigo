using UnityEngine;
using System.Collections.Generic;

public class RagDollController : MonoBehaviour
{
    [SerializeField] private Transform world;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform skeleton;
    [SerializeField] private Transform mainRoot;
    [SerializeField] private Transform hips;
    [SerializeField] private Transform armsRoot;
    [SerializeField] private Transform targetBone;
    [SerializeField] private Transform neckPeak;

    [SerializeField] private Transform spine3;
    [SerializeField] private List<Transform> shoulders = new List<Transform>();

    [SerializeField] private List<Transform> iks = new List<Transform>();

    [SerializeField] private List<Transform> bones = new List<Transform>();

    private Rigidbody[] rigidBodies;
    private Collider[] colliders;
    private Joint[] joints; 

    private void Awake()
    {
        rigidBodies = transform.GetComponentsInChildren<Rigidbody>();
        colliders = transform.GetComponentsInChildren<Collider>();
        joints = transform.GetComponentsInChildren<Joint>();

        DisableRagdoll();
    }

    public void DisableRagdoll()
    {
        SwitchOff();
        DisableColliders();
        DisableJoints();

        animator.enabled = true;
    }

    private void SwitchOff()
    {
        foreach (Rigidbody body in rigidBodies)
        {
            if (body.gameObject.layer == (int)Layers.RagDoll){
                body.velocity = Vector3.zero;
                body.isKinematic = true;
            }
        }
    }

    private void DisableColliders()
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == (int)Layers.RagDoll){
                collider.enabled = false;
            }
        }
    }

    private void DisableJoints()
    {
        foreach (Joint joint in joints){
            joint.enableCollision = false;
        }
    }

    public void SwitchOn()
    {
        animator.enabled = false;

        EnableComponents();
        ChangeParents();
    }

    private void EnableComponents()
    {
        foreach (Transform bone in bones)
            SetUpBone(bone);
    }

    private void SetUpBone(Transform bone)
    {
        if (bone != null)
        {
            Rigidbody rigidBone = bone.GetComponent<Rigidbody>();
            Collider colliderBone = bone.GetComponent<Collider>();
            Joint joint = bone.GetComponent<Joint>();

            if (rigidBone != null) rigidBone.velocity = Vector3.zero;
            if (rigidBone != null) rigidBone.isKinematic = false;
            if (colliderBone != null) colliderBone.enabled = true;
            if (joint != null) joint.enableCollision = true;
        }
    }

    private void ChangeParents()
    {
        targetBone.SetParent(neckPeak);
        hips.SetParent(skeleton);
        armsRoot.SetParent(skeleton);
        transform.SetParent(world);

        foreach (Transform ik in iks)
            ik.SetParent(skeleton);

        foreach (Transform shoulder in shoulders)
            shoulder.SetParent(spine3);
    }
}
