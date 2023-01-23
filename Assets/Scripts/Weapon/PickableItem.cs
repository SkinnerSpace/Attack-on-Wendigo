using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [SerializeField] private Transform portablePart;
    [SerializeField] private List<GameObject> layerTransitiveParts;
    [SerializeField] private Rigidbody physics;

    float currentTime = 0f;
    float pickTime = 0.3f;

    private Vector3 targetPosition;
    private Vector3 originalPos;

    private Action onPickedUp;

    public void PickUp(IHolder holder, Action onPickedUp)
    {
        this.onPickedUp = onPickedUp;

        StickToAPoint(holder);
        StartCoroutine(ComeIntoPosition());
    }

    private void StickToAPoint(IHolder holder)
    {
        SetPhysics(false);

        transform.SetParent(holder.transform, true);
        originalPos = transform.localPosition;
        targetPosition = holder.targetPosition;

        foreach(GameObject part in layerTransitiveParts)
            part.layer = LayerMask.NameToLayer("Weapon");
    }

    private IEnumerator ComeIntoPosition()
    {
        while (currentTime < pickTime)
        {
            currentTime += Chronos.DeltaTime;
            float transition = GetTransitionFrom(currentTime);
            Displace(transition);

            yield return null;
        }

        SetFinalPosition();
        onPickedUp();
    }

    private float GetTransitionFrom(float currentTime)
    {
        float transition = Mathf.InverseLerp(0f, pickTime, currentTime);
        transition = FadeOut(transition);

        return transition;
    }

    private float FadeOut(float value) => Mathf.Sqrt(value);

    private void Displace(float transition)
    {
        transform.localPosition = Vector3.Lerp(originalPos, targetPosition, transition);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), transition);
    }

    private void SetFinalPosition()
    {
        transform.localPosition = Vector3.zero;
        portablePart.localPosition = targetPosition;
    }

    private void SetPhysics(bool active)
    {
        if (active){
            physics.isKinematic = false;
            physics.useGravity = true;
        }
        else{
            physics.isKinematic = true;
            physics.useGravity = false;
        }
    }
}
