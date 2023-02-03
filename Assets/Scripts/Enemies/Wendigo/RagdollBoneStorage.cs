using UnityEngine;

[ExecuteAlways]
public class RagdollBoneStorage : MonoBehaviour
{
    public RagdollBone[] ragdollBones;
    public RagdollPuller[] pullers;

    public bool isReady { get; private set; }

    private void OnEnable()
    {
        isReady = false;
        FindAllTheBones();
        FindAllThePullers();
        LinkBones();
    }

    private void OnDisable() => isReady = false;

    private void FindAllTheBones() => ragdollBones = transform.GetComponentsInChildren<RagdollBone>();

    private void FindAllThePullers() => pullers = transform.GetComponentsInChildren<RagdollPuller>();

    private void LinkBones(){
        foreach (RagdollBone bone in ragdollBones)
            bone.LinkTheStorage(this);

        isReady = true;
    }
}
