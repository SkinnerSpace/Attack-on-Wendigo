using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    [SerializeField] private Transform handyItemImp;
    [SerializeField] private Pickable pickable;
    [SerializeField] private RetractionController RetractablePoint;
    [SerializeField] private List<SkinnedMeshRenderer> armsRenderers;

    private IHandyItem handyItem;
    private Transform oldParent;
    private Transform newParent;

    private void Awake()
    {
        handyItem = handyItemImp.GetComponent<IHandyItem>();

        oldParent = transform.parent;

        pickable.onPickedUp += ShowArms;
        pickable.onDropped += HideArms;

        handyItem.SubscribeOnReady(ResetOldParent);
    }

    private void ShowArms()
    {
        SetNewParent();
        RetractablePoint.Extend();
        EnableRenderers();
    }

    private void SetNewParent()
    {
        newParent = pickable.HoldParent; 

        transform.parent = newParent;
        transform.localPosition = handyItem.DefaultPosition;
        transform.localRotation = Quaternion.identity;
    }


    private void EnableRenderers()
    {
        foreach (SkinnedMeshRenderer armRenderer in armsRenderers)
            armRenderer.enabled = true;
    }

    private void HideArms(){
        SetNewParent();
        RetractablePoint.Retract(BackToWeapon);
    }

    private void BackToWeapon()
    {
        DisableRenderers();
        ResetOldParent();
    }

    private void DisableRenderers()
    {
        foreach (SkinnedMeshRenderer armRenderer in armsRenderers)
            armRenderer.enabled = false;
    }

    public void ResetOldParent() => transform.parent = oldParent;
}
