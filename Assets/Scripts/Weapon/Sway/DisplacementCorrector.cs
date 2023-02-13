using System.Collections;
using UnityEngine;

public class DisplacementCorrector : MonoBehaviour
{
    private float resetTime = 0.5f;
    private float currentTime = 0f;

    private Vector3 twistedPosition;
    private Quaternion twistedRotation;

    private Vector3 fixedPosition = Vector3.zero;
    private Quaternion fixedRotation = Quaternion.identity;

    private IWeapon weapon;

    public void Fix(IWeapon weapon)
    {
        this.weapon = weapon;
        StartCoroutine(Fixing());
    }

    private IEnumerator Fixing()
    {
        InitializeOriginalTwist();

        while (IsNotReady())
        {
            DoTransition();
            yield return null;
        }

        ResetTime();
    }

    private void InitializeOriginalTwist()
    {
        twistedPosition = transform.localPosition;
        twistedRotation = transform.localRotation;
    }

    private bool IsNotReady() => !weapon.isReady && currentTime < resetTime;

    private void DoTransition()
    {
        currentTime += OldChronos.DeltaTime;
        Move(GetTransition());
    }

    private float GetTransition() => Mathf.InverseLerp(0f, resetTime, currentTime);

    private void Move(float transition)
    {
        transform.localPosition = Vector3.Lerp(twistedPosition, fixedPosition, transition);
        transform.localRotation = Quaternion.Lerp(twistedRotation, fixedRotation, transition);
    }

    private void ResetTime() => currentTime = 0f;
}
