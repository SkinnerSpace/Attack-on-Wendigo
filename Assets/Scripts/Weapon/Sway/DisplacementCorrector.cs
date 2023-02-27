using System.Collections;
using UnityEngine;

public class DisplacementCorrector : MonoBehaviour
{
    private float resetTime = 0.5f;
    private float currentTime = 0f;

    private Vector3 twistedPosition;
    private Quaternion twistedRotation;

    private Vector3 fixedPosition;
    private Quaternion fixedRotation;

    private WeaponData data;

    private void Awake()
    {
        fixedPosition = transform.localPosition;
        fixedRotation = transform.localRotation;
    }

    public void Fix(WeaponData data)
    {
        this.data = data;
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

    private bool IsNotReady() => !data.IsReady && currentTime < resetTime;

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
