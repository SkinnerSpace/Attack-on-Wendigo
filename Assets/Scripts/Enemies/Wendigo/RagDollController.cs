using UnityEngine;
using System.Collections.Generic;

public class RagDollController : MonoBehaviour
{
    [SerializeField] private Transform world;
    [SerializeField] private Animator animator;

    [SerializeField] private List<Transform> bones = new List<Transform>();

    [SerializeField] private Transform targetBone;
    [SerializeField] private Transform destination;

    private Rigidbody[] rigidBodies;
    private Collider[] colliders;
    private Joint[] joints; 

    private void Awake()
    {
        rigidBodies = transform.GetComponentsInChildren<Rigidbody>();
        colliders = transform.GetComponentsInChildren<Collider>();
        joints = transform.GetComponentsInChildren<Joint>();

        SetActive(false);
    }

    public void SetActive(bool enabled)
    {
        foreach (Transform bone in bones)
        {
            Rigidbody rigidBone = bone.GetComponent<Rigidbody>();
            Collider colliderBone = bone.GetComponent<Collider>();
            Joint joint = bone.GetComponent<Joint>();

            if (rigidBone != null) rigidBone.velocity = Vector3.zero;
            if (rigidBone != null) rigidBone.isKinematic = !enabled;
            if (colliderBone != null) colliderBone.enabled = enabled;
            if (joint != null) joint.enableCollision = enabled;
        }

        if (enabled)
        {
            targetBone.SetParent(destination);
            //transform.SetParent(world);
        }

        if (!enabled)
        {
            foreach (Rigidbody body in rigidBodies)
            {
                if (body.gameObject.layer == (int)Layers.RagDoll)
                {
                    body.velocity = Vector3.zero;
                    body.isKinematic = !enabled;
                }
            }

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.layer == (int)Layers.RagDoll)
                {
                    collider.enabled = enabled;
                }
            }

            foreach (Joint joint in joints)
            {
                joint.enableCollision = enabled;
            }
        }

        animator.enabled = !enabled;
    }
}
