using System;
using System.Collections;
using UnityEngine;

public class ItemTransitionController : MonoBehaviour
{
    private Action onPickedUp;

    private float currentTime = 0f;
    private float pickTime = 0.3f;

    private Vector3 originalPos;
    private Vector3 targetPos;

    private bool isMoving;

    public void Launch(IPickable item, Action onPickedUp)
    {
        this.onPickedUp = onPickedUp;

        originalPos = transform.localPosition;
        targetPos = item.Get<Weapon>().DefaultPosition;

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
        transform.localPosition = Vector3.Lerp(originalPos, targetPos, transition);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), transition);
    }
}