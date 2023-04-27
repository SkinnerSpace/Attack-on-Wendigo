﻿using System;
using System.Collections;
using UnityEngine;

public class ItemTransitionController : MonoBehaviour
{
    private IHandyItem handyItem;

    private Transform item;

    private float currentTime = 0f;
    private float pickTime = 0.3f;

    private Vector3 originalPos;
    private Vector3 targetPos;

    private bool isMoving;

    private Action onPickedUp;

    private void Awake(){
        handyItem = GetComponentInParent<IHandyItem>();
    }

    public void Launch(Transform item, Action onPickedUp)
    {
        this.onPickedUp = onPickedUp;
        this.item = item;

        originalPos = item.localPosition;
        targetPos = handyItem.DefaultPosition;

        currentTime = 0f;
        isMoving = true;
        StartCoroutine(ComeIntoPosition());
    }

    public void Stop() => isMoving = false;

    private IEnumerator ComeIntoPosition()
    {
        while (currentTime < pickTime && isMoving)
        {
            CarryOn();
            yield return null;
        }

        onPickedUp();
    }

    private void CarryOn()
    {
        currentTime += OldChronos.DeltaTime;
        float transition = GetTransition(currentTime);
        Displace(transition);
    }

    private float GetTransition(float currentTime)
    {
        float transition = Mathf.InverseLerp(0f, pickTime, currentTime);
        transition = FadeOut(transition);

        return transition;
    }

    private float FadeOut(float value) => Mathf.Sqrt(value);

    public void Displace(float transition)
    {
        item.localPosition = Vector3.Lerp(originalPos, targetPos, transition);
        item.localRotation = Quaternion.Slerp(item.localRotation, Quaternion.identity, transition);
    }
}