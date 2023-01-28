using System;
using System.Collections;
using UnityEngine;

public class ItemTransitionController : MonoBehaviour
{
    private ItemPositionSetter positionSetter;
    private Action onPickedUp;

    private float currentTime = 0f;
    private float pickTime = 0.3f;

    public void Launch(ItemPositionSetter positionSetter, Action onPickedUp)
    {
        this.positionSetter = positionSetter;
        this.onPickedUp = onPickedUp;

        currentTime = 0f;
        StartCoroutine(ComeIntoPosition());
    }

    private IEnumerator ComeIntoPosition()
    {
        while (currentTime < pickTime)
        {
            CarryOn();
            yield return null;
        }

        onPickedUp();
    }

    private void CarryOn()
    {
        currentTime += Chronos.DeltaTime;
        float transition = GetTransition(currentTime);
        positionSetter.Displace(transition);
    }

    private float GetTransition(float currentTime)
    {
        float transition = Mathf.InverseLerp(0f, pickTime, currentTime);
        transition = FadeOut(transition);

        return transition;
    }

    private float FadeOut(float value) => Mathf.Sqrt(value);
}